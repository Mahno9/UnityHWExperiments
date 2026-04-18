using System;
using System.Collections.Generic;
using System.Linq;

using MiniGame.Characters;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    // TODO: make readonly interface
    public class EnemiesService : IUpdatable, IDisposable
    {
        private readonly List<EnemyCharacter> _enemies = new();

        private readonly EnemySpawner              _spawner;
        private readonly SpawnPoseGeneratorService _spawnPoseGenerator;

        public EnemiesService(EnemySpawner spawner, SpawnPoseGeneratorService spawnPoseGenerator)
        {
            _spawner = spawner;
            _spawnPoseGenerator = spawnPoseGenerator;
        }

        public int EnemiesCount => _enemies.Count;

        public void SpawnEnemy()
        {
            _enemies.AddRange(
                _spawner.Spawn(GetRandomPoseOnLevel())
            );
        }

        public void SpawnEnemies(int amount)
        {
            _enemies.AddRange(
                _spawner.Spawn(
                    Enumerable.Range(0, amount)
                        .Select(_ => GetRandomPoseOnLevel())
                        .ToArray()
                )
            );
        }

        public void Update(float deltaTime)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i].IsDead.Value == false)
                    continue;

                _enemies[i].Destroy();
                _enemies.RemoveAt(i);
            }
        }

        private Pose GetRandomPoseOnLevel()
        {
            return _spawnPoseGenerator.GetRandomSpawnPointWithExcluding();
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
                enemy.Destroy();
        }
    }
}