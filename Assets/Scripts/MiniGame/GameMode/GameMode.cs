using System;
using System.Collections.Generic;
using System.Linq;

using Delegates.Timer;

using MiniGame.Characters;
using MiniGame.Configs;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    public class GameMode : IUpdatable
    {
        public event Action Win;
        public event Action Defeat;

        private readonly LevelConfig   _levelConfig;
        private readonly EnemySpawner  _spawner;
        private readonly IHavePosition _mainCharacterPositioner;

        private readonly List<EnemyCharacter> _enemies;
        private readonly TimerService         _spawnTimer;
        private readonly Level                _level;

        public GameMode(LevelConfig levelConfig, CharactersFactory charactersFactory, EnemyCharacterConfig enemyCharacterConfig, Level level, IHavePosition mainCharacterPositioner)
        {
            _levelConfig = levelConfig;
            _level = level;
            _mainCharacterPositioner = mainCharacterPositioner;

            _enemies = new List<EnemyCharacter>();
            _spawner = new EnemySpawner(charactersFactory, enemyCharacterConfig);
            _spawnTimer = new TimerService();
            _spawnTimer.OnTimerStopped += OnSpawnTimerTick;
        }

        private void OnSpawnTimerTick()
        {
            _enemies.AddRange(_spawner.Spawn(GetRandomPoseOnLevel()));

            StartSpawnTimer();
        }

        public void Start()
        {
            _enemies.AddRange(
                _spawner.Spawn(
                    Enumerable.Range(0, _levelConfig.StartEnemiesCount)
                        .Select(_ => GetRandomPoseOnLevel())
                        .ToArray()
                )
            );

            StartSpawnTimer();
        }

        private void ProcessGameEnd()
        {
            foreach (var enemy in _enemies)
                enemy.Destroy();
        }

        public void Update(float deltaTime)
        {
            _spawnTimer.Update(deltaTime);
        }

        private Pose GetRandomPoseOnLevel()
        {
            return _level.GetRandomSpawnPointExcluding(_mainCharacterPositioner.Position, _levelConfig.MainCharacterSpawnExcludeRadius);
        }

        private void StartSpawnTimer()
        {
            _spawnTimer.StartTimer(_levelConfig.EnemiesSpawnDelay);
        }
    }
}