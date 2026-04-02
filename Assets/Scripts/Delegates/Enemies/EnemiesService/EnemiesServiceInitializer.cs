using System;
using System.Collections.Generic;

using Delegates.Enemies.Enemy;

using UnityEngine;
using UnityEngine.Serialization;

namespace Delegates.Enemies.EnemiesService
{
    public class EnemiesServiceInitializer : MonoBehaviour
    {
        [SerializeField] private List<Enemy.Enemy> _enemiesPrefabs;
        [SerializeField] private AreaEnemySpawner  _spawner;

        private EnemiesService           _service;
        private EnemiesServiceTestInputs _testInputs;

        void Awake()
        {
            _service = new EnemiesService();
            _testInputs = new EnemiesServiceTestInputs(_service, _enemiesPrefabs, _spawner, transform);
        }

        private void Update()
        {
            _testInputs.Update(Time.deltaTime);
            _service.Update(Time.deltaTime);
        }

        public EnemiesService GetService() => _service;
    }
}