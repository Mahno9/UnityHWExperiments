using System;
using System.Collections;

using MiniGame.Characters;
using MiniGame.Configs;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame
{
    public class GameplayCycle : IUpdatable, IDisposable
    {
        private readonly CharactersFactory         _charactersFactory;
        private readonly MainCharacterConfig       _mainCharacterConfig;
        private readonly LevelConfig               _levelConfig;
        private readonly EnemyCharacterConfig      _enemyCharacterConfig;
        private readonly SpawnPoseGeneratorService _spawnPoseGenerator;
        private readonly MonoBehaviour             _coroutineRunner;

        private          GameMode      _gameMode;

        public GameplayCycle(
            CharactersFactory         charactersFactory,
            MainCharacterConfig       mainCharacterConfig,
            EnemyCharacterConfig      enemyCharacterConfig,
            LevelConfig               levelConfig,
            SpawnPoseGeneratorService spawnPoseGenerator,
            MonoBehaviour             coroutineRunner)
        {
            _charactersFactory = charactersFactory;
            _mainCharacterConfig = mainCharacterConfig;
            _levelConfig = levelConfig;
            _enemyCharacterConfig = enemyCharacterConfig;
            _spawnPoseGenerator = spawnPoseGenerator;
            _coroutineRunner = coroutineRunner;

            _gameMode = new GameMode(_levelConfig, _charactersFactory, _enemyCharacterConfig, _spawnPoseGenerator, _mainCharacterConfig);
        }

        public void Dispose()
        {
            ProcessFinish();
        }

        public IEnumerator Launch()
        {
            // Confirm pop up window here

            _gameMode.Start();

            _gameMode.Win.Changed += OnGameModeWin;
            _gameMode.Defeat.Changed += OnGameModeDefeat;

            yield return null;
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        private void OnGameModeWin(bool b, bool b1)
        {
            if (b1 == false)
                return;

            ProcessFinish();
            Debug.Log("Win!");
            _coroutineRunner.StartCoroutine(Launch());
        }

        private void OnGameModeDefeat(bool b, bool b1)
        {
            if (b1 == false)
                return;

            ProcessFinish();
            Debug.Log("Defeat!");
            _coroutineRunner.StartCoroutine(Launch());
        }

        private void ProcessFinish()
        {
            if (_gameMode is not null)
            {
                _gameMode.ProcessGameEnd();

                _gameMode.Win.Changed -= OnGameModeWin;
                _gameMode.Defeat.Changed -= OnGameModeDefeat;
            }
        }
    }
}