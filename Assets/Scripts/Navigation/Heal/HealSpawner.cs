using System;
using System.Collections;

using Unity.AI.Navigation;

using UnityEngine;
using UnityEngine.AI;

using Object = UnityEngine.Object;

namespace Navigation.Heal
{
    public class HealSpawner
    {
        private readonly Transform _spawnTransform;
        public HealSpawner(Transform spawnTransform)
        {
            _spawnTransform = spawnTransform;
        }

        public IEnumerator ProcessHealSpawn(float spawnDelay, GameObject healPrefab, float spawnRadius)
        {
            float timeTilSpawn = spawnDelay;

            while (timeTilSpawn > 0)
            {
                Debug.Log($"Time till spawn heal: {timeTilSpawn}s");
                timeTilSpawn -= Time.deltaTime;
                yield return null;
            }

            Debug.Log("Heal spawned!");
            Spawn(healPrefab, spawnRadius);
        }

        private void Spawn(GameObject healPrefab, float spawnRadius)
        {
            Object.Instantiate(healPrefab, PickPosition(spawnRadius), healPrefab.transform.rotation);
        }

        private Vector3 PickPosition(float spawnRadius)
        {
            int tries = 100;

            do
            {
                Vector3 sourcePosition = UnityEngine.Random.insideUnitSphere * spawnRadius;
                sourcePosition += _spawnTransform.position;

                bool isPositionFound = NavMesh.SamplePosition(sourcePosition, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas);

                if (isPositionFound)
                    return hit.position;

                tries--;
            } while (tries > 0);

            return _spawnTransform.position;
        }
    }
}