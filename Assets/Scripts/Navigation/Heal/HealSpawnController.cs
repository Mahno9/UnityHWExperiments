using System;

using UnityEngine;

namespace Navigation.Heal
{
    public class HealSpawnController : MonoBehaviour
    {
        [SerializeField] private GameObject _healPrefab;
        [SerializeField] private KeyCode    _callHealKey = KeyCode.F;
        [SerializeField] private float      _spawnDelay  = 2.0f;
        [SerializeField] private float      _spawnRadius = 5.0f;

        private HealSpawner _spawner;

        private void Awake()
        {
            _spawner = new HealSpawner(transform);
        }

        public void Update()
        {
            if (Input.GetKeyDown(_callHealKey))
                StartCoroutine(_spawner.ProcessHealSpawn(_spawnDelay, _healPrefab, _spawnRadius));
        }
    }
}