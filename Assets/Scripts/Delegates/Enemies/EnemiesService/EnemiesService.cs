using System;
using System.Collections.Generic;

using Navigation.Controllers;

using UnityEngine;

namespace Delegates.Enemies.EnemiesService
{
    using Enemy = Enemy.Enemy;
    using Object = UnityEngine.Object;

    public class EnemiesService: IUpdatable
    {
        private readonly List<Enemy> _enemies = new();

        private readonly List<Enemy> _enemiesToRemove = new();

        public int EnemiesCount => _enemies.Count;

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            enemy.OnDie += OnEnemyDie;
        }

        private void OnEnemyDie(Enemy enemy)
        {
            _enemiesToRemove.Add(enemy);
        }

        public void Update(float deltaTime)
        {
            ClearDeadEnemies();
        }

        private void ClearDeadEnemies()
        {
            foreach (Enemy enemy in _enemiesToRemove)
            {
                _enemies.Remove(enemy);
                Object.Destroy(enemy.gameObject);
            }
            _enemiesToRemove.Clear();
        }
    }
}