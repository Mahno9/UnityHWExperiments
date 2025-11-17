using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HighlightItem : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private float _highlightSize = 0.2f;

    private const string HIGHLIGHT_CHILD_NAME = "Highlight";

    private void Start()
    {
        CreateHighlightCopy();
    }

    private void CreateHighlightCopy()
    {
        GameObject highlightCopy = new GameObject(HIGHLIGHT_CHILD_NAME);
        highlightCopy.transform.SetParent(transform);
        highlightCopy.transform.localPosition = Vector3.zero;
        highlightCopy.transform.localRotation = Quaternion.identity;
        highlightCopy.transform.localScale = Vector3.one * (1 + _highlightSize);

        MeshFilter originalMeshFilter = GetComponent<MeshFilter>();

        MeshFilter copyMeshFilter = highlightCopy.AddComponent<MeshFilter>();
        copyMeshFilter.sharedMesh = originalMeshFilter.sharedMesh;

        MeshRenderer copyMeshRenderer = highlightCopy.AddComponent<MeshRenderer>();
        copyMeshRenderer.materials = new Material[] { _highlightMaterial };
    }
}
