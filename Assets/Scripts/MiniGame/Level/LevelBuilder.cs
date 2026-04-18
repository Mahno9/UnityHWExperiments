using Unity.AI.Navigation;

using UnityEngine;

namespace MiniGame
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject     _floorPrefab;
        [SerializeField] private GameObject     _wallPrefab;
        [SerializeField] private Transform      _levelRoot;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        // AI Generated
        public Level BuildLevelBox(int width, int height)
        {
            if (_floorPrefab is null || _wallPrefab is null || _levelRoot is null) return null;

            Vector3 floorSize = GetPrefabSize(_floorPrefab);
            Vector3 wallSize  = GetPrefabSize(_wallPrefab);

            float cellSizeX = floorSize.x;
            float cellSizeZ = floorSize.z;

            Level level      = new(new Vector2(cellSizeX, cellSizeZ));
            float wallLength = wallSize.x;

            float totalWidth  = width * cellSizeX;
            float totalHeight = height * cellSizeZ;

            // Floor filling centered at _levelRoot
            for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
            {
                float localX = (x - (width - 1) / 2f) * cellSizeX;
                float localZ = (z - (height - 1) / 2f) * cellSizeZ;

                GameObject floor = Instantiate(_floorPrefab, _levelRoot);
                floor.transform.localPosition = new Vector3(localX, 0, localZ);
                floor.transform.localRotation = Quaternion.identity;
                level.AddFloorTile(floor.transform);
            }

            // Boundary limits in local space
            float xMin = -totalWidth / 2f;
            float xMax = totalWidth / 2f;
            float zMin = -totalHeight / 2f;
            float zMax = totalHeight / 2f;

            // Walls bottom and top (along X axis)
            for (float x = 0; x < totalWidth; x += wallLength)
            {
                float currentX = x;
                if (currentX + wallLength > totalWidth) currentX = Mathf.Max(0, totalWidth - wallLength);

                float localPosX = xMin + currentX + wallLength / 2f;

                // Bottom
                GameObject wallBottom = Instantiate(_wallPrefab, _levelRoot);
                wallBottom.transform.localPosition = new Vector3(localPosX, 0, zMin);
                wallBottom.transform.localRotation = Quaternion.identity;

                // Top
                GameObject wallTop = Instantiate(_wallPrefab, _levelRoot);
                wallTop.transform.localPosition = new Vector3(localPosX, 0, zMax);
                wallTop.transform.localRotation = Quaternion.Euler(0, 180, 0);

                if (currentX + wallLength >= totalWidth) break;
            }

            // Walls left and right (along Z axis)
            for (float z = 0; z < totalHeight; z += wallLength)
            {
                float currentZ = z;
                if (currentZ + wallLength > totalHeight) currentZ = Mathf.Max(0, totalHeight - wallLength);

                float localPosZ = zMin + currentZ + wallLength / 2f;

                // Left
                GameObject wallLeft = Instantiate(_wallPrefab, _levelRoot);
                wallLeft.transform.localPosition = new Vector3(xMin, 0, localPosZ);
                wallLeft.transform.localRotation = Quaternion.Euler(0, 90, 0);

                // Right
                GameObject wallRight = Instantiate(_wallPrefab, _levelRoot);
                wallRight.transform.localPosition = new Vector3(xMax, 0, localPosZ);
                wallRight.transform.localRotation = Quaternion.Euler(0, 270, 0);

                if (currentZ + wallLength >= totalHeight) break;
            }

            _navMeshSurface?.BuildNavMesh();

            return level;
        }

        private Vector3 GetPrefabSize(GameObject prefab)
        {
            Renderer renderer = prefab.GetComponentInChildren<Renderer>();
            return renderer is not null ? renderer.bounds.size : Vector3.one;
        }
    }
}