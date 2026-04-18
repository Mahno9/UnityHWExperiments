using System;
using System.Collections.Generic;
using System.Linq;

using MiniGame.Characters;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    public class EnemiesService : IUpdatable
    {
        public event Action OnEnemyKilled;

        private readonly LinkedList<EnemyCharacter> _enemies = new();

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
            foreach (EnemyCharacter enemy in _spawner.Spawn(GetRandomPoseOnLevel()))
                _enemies.AddLast(enemy);
            Debug.Log($"Enemies count: {EnemiesCount}");
        }

        public void SpawnEnemies(int amount)
        {
            Pose[] poses = Enumerable.Range(0, amount).Select(_ => GetRandomPoseOnLevel()).ToArray();
            foreach (EnemyCharacter enemy in _spawner.Spawn(poses))
                _enemies.AddLast(enemy);
            Debug.Log($"Enemies count: {EnemiesCount}");
        }

        public void Update(float deltaTime)
        {
            LinkedListNode<EnemyCharacter> node = _enemies.First;
            while (node != null)
            {
                LinkedListNode<EnemyCharacter> next = node.Next;

                if (node.Value.IsDead.Value)
                {
                    node.Value.Destroy();
                    _enemies.Remove(node);

                    OnEnemyKilled?.Invoke();
                }

                node = next;
            }
        }

        private Pose GetRandomPoseOnLevel()
        {
            return _spawnPoseGenerator.GetRandomSpawnPointWithExcluding();
        }

        public void DestroyEnemies()
        {
            foreach (EnemyCharacter enemy in _enemies)
            {
                if (enemy != null)
                    enemy.Destroy();
            }

            _enemies.Clear();
        }
    }
}