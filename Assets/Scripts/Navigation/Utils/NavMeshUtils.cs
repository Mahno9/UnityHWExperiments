using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Utils
{
    public static class NavMeshUtils
    {
        public static bool TryGetPath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter queryFilter, NavMeshPath outPath)
        {
            return NavMesh.CalculatePath(sourcePosition, targetPosition, queryFilter, outPath) && outPath.status != NavMeshPathStatus.PathInvalid;
        }
    }
}