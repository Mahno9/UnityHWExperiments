using System.Collections.Generic;
using UnityEngine;

namespace MiniGame
{
    public class Level
    {
        private readonly List<Transform> _floorTiles = new();
        private readonly Vector2         _tileSize;

        public Level(Vector2 tileSize)
        {
            _tileSize = tileSize;
        }

        public void AddFloorTile(Transform tile)
        {
            _floorTiles.Add(tile);
        }

        public Pose GetRandomSpawnPoint()
        {
            Transform tile = _floorTiles[Random.Range(0, _floorTiles.Count)];
            return new Pose(RandomPointOnTile(tile), RandomRotation());
        }

        public Pose GetRandomSpawnPointExcluding(Vector3 excludeCenter, float excludeRadius)
        {
            float sqrRadius = excludeRadius * excludeRadius;

            List<Transform> candidates = _floorTiles.FindAll(
                t => (t.position - excludeCenter).sqrMagnitude > sqrRadius
            );

            if (candidates.Count == 0)
                return GetRandomSpawnPoint();

            Transform tile = candidates[Random.Range(0, candidates.Count)];
            return new Pose(RandomPointOnTile(tile), RandomRotation());
        }

        private Vector3 RandomPointOnTile(Transform tile)
        {
            float offsetX = Random.Range(-_tileSize.x / 2f, _tileSize.x / 2f);
            float offsetZ = Random.Range(-_tileSize.y / 2f, _tileSize.y / 2f);
            return tile.position + new Vector3(offsetX, 0, offsetZ);
        }

        private static Quaternion RandomRotation() => Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}
