using UnityEngine;
using UnityEngine.AI;


public static class NavMeshUtils
{
    public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath outPath)
    {
        return NavMesh.CalculatePath(sourcePosition, targetPosition, queryFilter, outPath) && outPath.status != NavMeshPathStatus.PathInvalid;
    }
}