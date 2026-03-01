using System.Collections.Generic;

namespace Delegates.Enemies.EnemiesService
{
    public class EnemiesService
    {
        private readonly List<Enemy.Enemy> _enemies = new();

        public int EnemiesCount => _enemies.Count;

    }
}