using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public static class ResourcesReferenceGenerator
    {
        private const string OutputPath = "Assets/Scripts/Generated/R.cs";

        static ResourcesReferenceGenerator()
        {
            Generate();
        }

        [MenuItem("Tools/Regenerate R.cs")]
        public static void Generate()
        {
            List<string> resourcePaths = CollectResourcePaths();
            string code = BuildCode(resourcePaths);
            WriteIfChanged(code);
        }

        private static List<string> CollectResourcePaths()
        {
            var paths = new List<string>();
            string[] guids = AssetDatabase.FindAssets("", new[] { "Assets" });

            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                if (AssetDatabase.IsValidFolder(assetPath)) continue;
                if (assetPath.EndsWith(".meta")) continue;

                int resourcesIndex = assetPath.LastIndexOf("/Resources/");
                if (resourcesIndex < 0) continue;

                string relativePath = assetPath.Substring(resourcesIndex + "/Resources/".Length);
                string withoutExt   = Path.GetFileNameWithoutExtension(relativePath);
                string folder       = Path.GetDirectoryName(relativePath)?.Replace('\\', '/');

                string resourcePath = string.IsNullOrEmpty(folder)
                    ? withoutExt
                    : $"{folder}/{withoutExt}";

                paths.Add(resourcePath);
            }

            paths.Sort();
            return paths;
        }

        private static string BuildCode(List<string> resourcePaths)
        {
            Node root = new("R");

            foreach (string path in resourcePaths)
            {
                string[] parts   = path.Split('/');
                Node     current = root;

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    string folderName = Sanitize(parts[i]);
                    if (!current.Children.TryGetValue(folderName, out Node child))
                    {
                        child                        = new Node(folderName);
                        current.Children[folderName] = child;
                    }

                    current = child;
                }

                string constName = Sanitize(parts[parts.Length - 1]);
                current.Constants[constName] = path;
            }

            var sb = new StringBuilder();
            sb.AppendLine("// AUTO-GENERATED — DO NOT EDIT");
            sb.AppendLine("// Re-generated on each compilation via ResourcesReferenceGenerator.cs");
            sb.AppendLine();
            sb.AppendLine("public static class R");
            sb.AppendLine("{");
            WriteNode(root, sb, 1);
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static void WriteNode(Node node, StringBuilder sb, int indent)
        {
            string pad = new(' ', indent * 4);

            foreach (KeyValuePair<string, string> constant in node.Constants)
                sb.AppendLine($"{pad}public const string {constant.Key} = \"{constant.Value}\";");

            foreach (KeyValuePair<string, Node> child in node.Children)
            {
                sb.AppendLine($"{pad}public static class {child.Key}");
                sb.AppendLine($"{pad}{{");
                WriteNode(child.Value, sb, indent + 1);
                sb.AppendLine($"{pad}}}");
            }
        }

        private static void WriteIfChanged(string code)
        {
            string directory = Path.GetDirectoryName(OutputPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(OutputPath) && File.ReadAllText(OutputPath) == code)
                return;

            File.WriteAllText(OutputPath, code);
            AssetDatabase.ImportAsset(OutputPath, ImportAssetOptions.ForceUpdate);
        }

        private static string Sanitize(string name)
        {
            string result = Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");
            if (result.Length > 0 && char.IsDigit(result[0]))
                result = "_" + result;
            return result;
        }

        private class Node
        {
            public readonly Dictionary<string, Node>   Children  = new();
            public readonly Dictionary<string, string> Constants = new();

            public Node(string name) { }
        }
    }
}
