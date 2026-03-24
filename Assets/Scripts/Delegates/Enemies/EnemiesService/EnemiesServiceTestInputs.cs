using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Delegates.Enemies.Enemy;

using JetBrains.Annotations;

using Navigation.Controllers;

using UnityEngine;
using UnityEngine.Assertions;

using Object = UnityEngine.Object;

namespace Delegates.Enemies.EnemiesService
{
    using Enemy = Enemy.Enemy;

    public class EnemiesServiceTestInputs : IUpdatable
    {
        private readonly List<Enemy>      _enemiesPrefabs;
        private readonly Transform        _enemiesParent;
        private readonly AreaEnemySpawner _spawner;
        private readonly EnemiesService   _service;

        public EnemiesServiceTestInputs(EnemiesService service, List<Enemy> enemiesPrefabs, AreaEnemySpawner spawner, Transform enemiesParent)
        {
            _service = service;
            _enemiesPrefabs = enemiesPrefabs;
            _spawner = spawner;
            _enemiesParent = enemiesParent;

            Assert.IsTrue(_enemiesPrefabs.Count >= 3);
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InstantiateWithDelegate(_enemiesPrefabs[0]);
            }

            if (Input.GetKeyDown(KeyCode.S))
                InstantiateWithDelegate(_enemiesPrefabs[1], () => _service.EnemiesCount <= 3);

            if (Input.GetKeyDown(KeyCode.D))
            {
                float secondsLeft = 2f;
                InstantiateWithDelegate(_enemiesPrefabs[2], () =>
                {
                    secondsLeft -= deltaTime;
                    return secondsLeft > 0;
                });
            }
        }

        private void InstantiateWithDelegate(Enemy enemyPrefab, Func<bool> aliveDelegate = null)
        {
            _spawner.SpawnEnemy(enemyPrefab, _enemiesParent, aliveDelegate);
        }
    }
}