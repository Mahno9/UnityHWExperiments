using UnityEngine;

public static class RuntimeDebugLine
{
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0f, float width = 0.03f)
    {
        GameObject go = new("RuntimeDebugLine");
        LineRenderer lr = go.AddComponent<LineRenderer>();

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = width;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.useWorldSpace = true;

        // Время жизни
        if (duration > 0f)
            Object.Destroy(go, duration);
    }
}
