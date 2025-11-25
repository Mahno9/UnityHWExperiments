using System.Collections.Generic;

using GhostWaiter.GameLogic.Holders;

using UnityEngine;
using UnityEngine.Assertions;

namespace GhostWaiter.GameLogic
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnHolders;
        [SerializeField] private Holdable.Holdable[] _spawnItems;
        [SerializeField] private float _spawnInterval = 2f;

        private float _spawnRemainingTime;

        private void Start()
        {
            Assert.IsTrue(_spawnHolders.Length > 0, "_spawnHolders is empty in Spawner.");
            Assert.IsTrue(_spawnItems.Length > 0, "_spawnItems is empty in Spawner.");
        }

        private void Update()
        {
            if (_spawnRemainingTime <= 0)
            {
                _spawnRemainingTime = _spawnInterval;
                SpawnItem();
            }

            _spawnRemainingTime -= Time.deltaTime;
        }

        private void SpawnItem()
        {
            SpawnPoint emptyHolder = FindEmptyHolder();
            if (emptyHolder is null)
                return;

            Holdable.Holdable itemPrefab = GetRandomSpawnItem();
            Holdable.Holdable spawnedItem = Instantiate(itemPrefab);

            emptyHolder.Put(spawnedItem);
        }

        private SpawnPoint FindEmptyHolder()
        {
            List<SpawnPoint> emptySpawnPoints = new List<SpawnPoint>();
            foreach (SpawnPoint holder in _spawnHolders)
            {
                if (holder.CanSpawn())
                    emptySpawnPoints.Add(holder);
            }

            if (emptySpawnPoints.Count == 0)
                return null;

            return emptySpawnPoints[UnityEngine.Random.Range(0, emptySpawnPoints.Count)];
        }

        private Holdable.Holdable GetRandomSpawnItem()
        {
            return _spawnItems[UnityEngine.Random.Range(0, _spawnItems.Length)];
        }

    }
}
