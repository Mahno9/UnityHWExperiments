using System;
using System.Collections.Generic;

using UnityEngine;

namespace Delegates.Enemies.EnemiesService
{
    public class EnemiesServiceInitializer : MonoBehaviour
    {
        [SerializeField] private List<Enemy.Enemy> _enemiesPrefabs;
        [SerializeField] private SpawnArea        _spawnArea;

        private EnemiesService           _service;
        private EnemiesServiceTestInputs _testInputs;

        void Awake()
        {
            _service = new EnemiesService();
            _testInputs = new EnemiesServiceTestInputs(_service, _enemiesPrefabs, _spawnArea, transform);
        }

        private void Update()
        {
            _testInputs.Update(Time.deltaTime);
            _service.Update(Time.deltaTime);
        }
    }
}