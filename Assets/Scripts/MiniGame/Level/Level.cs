using System.Collections.Generic;
using UnityEngine;

namespace MiniGame
{
    public class Level
    {
        private readonly List<Transform> _floorTiles = new();

        public void AddFloorTile(Transform tile)
        {
            _floorTiles.Add(tile);
        }

        public Pose GetRandomSpawnPoint()
        {
            Transform tile = _floorTiles[Random.Range(0, _floorTiles.Count)];
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            return new Pose(tile.position, rotation);
        }
    }
}
