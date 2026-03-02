using System;
using System.Collections.Generic;

using Navigation.Controllers;

namespace Delegates.Enemies.EnemiesService
{
    using Enemy = Enemy.Enemy;
    using Object = UnityEngine.Object;

    public class EnemiesService: IUpdatable
    {
        private readonly List<Enemy> _enemies = new();

        public int EnemiesCount => _enemies.Count;

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            enemy.OnDie += OnEnemyDie;
        }

        private void OnEnemyDie(Enemy enemy)
        {
            _enemies.Remove(enemy);
            Object.Destroy(enemy.gameObject);
        }

        public void Update(float deltaTime)
        {
            foreach (Enemy enemy in _enemies)
                enemy.Update();
        }
    }
}