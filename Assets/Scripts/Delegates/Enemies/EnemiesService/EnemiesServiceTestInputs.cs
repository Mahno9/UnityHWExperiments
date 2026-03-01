using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Navigation.Controllers;

using UnityEngine;
using UnityEngine.Assertions;

using Object = UnityEngine.Object;

namespace Delegates.Enemies.EnemiesService
{
    using Enemy = Enemy.Enemy;

    public class EnemiesServiceTestInputs : IUpdatable
    {
        private readonly EnemiesService _service;
        private readonly List<Enemy>    _enemiesPrefabs;
        private readonly SpawnArea      _spawnArea;
        private readonly Transform      _enemiesParent;

        public EnemiesServiceTestInputs(EnemiesService service, List<Enemy> enemiesPrefabs, SpawnArea spawnArea, Transform enemiesParent)
        {
            _service = service;
            _enemiesPrefabs = enemiesPrefabs;
            _spawnArea = spawnArea;
            _enemiesParent = enemiesParent;

            Assert.IsTrue(_enemiesPrefabs.Count >= 3);
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InstantiateWithDelegate(_enemiesPrefabs[0], enemy => enemy.IsDead() == false);
            }

            if (Input.GetKeyDown(KeyCode.S))
                InstantiateWithDelegate(_enemiesPrefabs[1], (_) => _service.EnemiesCount <= 3);

            if (Input.GetKeyDown(KeyCode.D))
            {
                float secondsLeft = 2f;
                InstantiateWithDelegate(_enemiesPrefabs[2], (_) =>
                {
                    secondsLeft -= deltaTime;
                    return secondsLeft > 0;
                });
            }
        }

        private void InstantiateWithDelegate(Enemy enemyPrefab, Func<Enemy, bool> aliveDelegate)
        {
            Vector3 spawnPoint = _spawnArea.GetRandomPoint();
            Enemy   newEnemy   = Object.Instantiate(enemyPrefab, spawnPoint, _enemiesParent.rotation, _enemiesParent);
            _service.AddEnemy(newEnemy, aliveDelegate);
        }
    }
}