using System;
using System.Collections;

using Unity.AI.Navigation;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Heal
{
    public class HealSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject     _healPrefab;
        [SerializeField] private KeyCode        _callHealKey = KeyCode.F;
        [SerializeField] private float          _spawnDelay  = 2.0f;
        [SerializeField] private float          _spawnRadius = 5.0f;

        private void Update()
        {
            if (Input.GetKeyDown(_callHealKey))
            {
                StartCoroutine(ProcessHealSpawn());
            }
        }

        private IEnumerator ProcessHealSpawn()
        {
            float timeTilSpawn = _spawnDelay;

            while (timeTilSpawn > 0)
            {
                timeTilSpawn -= Time.deltaTime;
                yield return null;
            }

            Spawn();
        }

        private void Spawn()
        {
            Instantiate(_healPrefab, PickPosition(), _healPrefab.transform.rotation);
        }

        private Vector3 PickPosition()
        {
            int tries = 100;

            do
            {
                Vector3 sourcePosition = UnityEngine.Random.insideUnitSphere * _spawnRadius;
                sourcePosition += transform.position;

                bool isPositionFound = NavMesh.SamplePosition(sourcePosition, out NavMeshHit hit, _spawnRadius, NavMesh.AllAreas);

                if (isPositionFound)
                    return hit.position;

                tries--;
            } while (tries > 0);

            return transform.position;
        }
    }
}