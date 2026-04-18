using System.Collections.Generic;

using UnityEngine;

namespace MiniGame
{
    public class Level
    {
        private readonly List<Transform> _floorTiles = new();

        public Level(Vector2 tileSize)
        {
            TileSize = tileSize;
        }

        public IReadOnlyList<Transform> FloorTiles => _floorTiles;
        public Vector2                  TileSize   { get; private set; }

        public void AddFloorTile(Transform tile)
        {
            _floorTiles.Add(tile);
        }
    }
}