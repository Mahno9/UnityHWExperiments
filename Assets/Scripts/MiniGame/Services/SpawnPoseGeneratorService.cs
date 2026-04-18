using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace MiniGame
{
    public class SpawnPoseGeneratorService
    {
        private readonly Level     _level;
        private          Transform _excludeTransform;
        private          float     _excludeRadius;

        public SpawnPoseGeneratorService(Level level)
        {
            _level = level;
        }

        public void Init(Transform excludeTransform, float excludeRadius)
        {
            _excludeTransform = excludeTransform;
            _excludeRadius = excludeRadius;
        }

        public Pose GetRandomSpawnPoint()
        {
            Transform tile = _level.FloorTiles[Random.Range(0, _level.FloorTiles.Count)];
            return new Pose(RandomPointOnTile(tile), RandomRotation());
        }

        public Pose GetRandomSpawnPointWithExcluding()
        {
            float   sqrRadius = _excludeRadius * _excludeRadius;

            List<Transform> candidates = _level.FloorTiles
                .Where(t => (t.position - _excludeTransform.position).sqrMagnitude > sqrRadius)
                .ToList();

            if (candidates.Count == 0)
                return GetRandomSpawnPoint();

            Transform tile = candidates[Random.Range(0, candidates.Count)];
            return new Pose(RandomPointOnTile(tile), RandomRotation());
        }

        private Vector3 RandomPointOnTile(Transform tile)
        {
            float offsetX = Random.Range(-_level.TileSize.x / 2f, _level.TileSize.x / 2f);
            float offsetZ = Random.Range(-_level.TileSize.y / 2f, _level.TileSize.y / 2f);
            return tile.position + new Vector3(offsetX, 0, offsetZ);
        }

        private static Quaternion RandomRotation() => Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}