using System;
using System.Collections.Generic;

using Navigation.Controllers;

namespace Delegates.Enemies.EnemiesService
{
    using AliveDelegate = Func<Enemy.Enemy, bool>;
    using Enemy = Enemy.Enemy;
    using EnemyInfo = KeyValuePair<Enemy.Enemy, Func<Enemy.Enemy, bool>>;
    using Object = UnityEngine.Object;

    public class EnemiesService :IUpdatable
    {
        private readonly List<EnemyInfo> _enemies = new();

        public int EnemiesCount => _enemies.Count;

        public void Update(float deltaTime)
        {
            CheckEnemiesForAliveness();
        }

        public void CheckEnemiesForAliveness()
        {
            List<EnemyInfo> enemiesToErase = new();
            foreach (EnemyInfo enemyAliveDelegate in _enemies)
            {
                if (enemyAliveDelegate.Value.Invoke(enemyAliveDelegate.Key) == false)
                    enemiesToErase.Add(enemyAliveDelegate);
            }

            foreach (EnemyInfo enemyAliveDelegate in enemiesToErase)
            {
                _enemies.Remove(enemyAliveDelegate);
                Object.Destroy(enemyAliveDelegate.Key.gameObject);
            }
        }

        public void AddEnemy(Enemy enemy, AliveDelegate isAliveDelegate)
        {
            _enemies.Add(new EnemyInfo(enemy, isAliveDelegate));
        }
    }
}