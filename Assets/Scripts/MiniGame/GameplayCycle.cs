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
        private readonly GameModeOptions           _gameModeOptions;
        private readonly CharactersFactory         _charactersFactory;
        private readonly LevelConfig               _levelConfig;
        private readonly EnemyCharacterConfig      _enemyCharacterConfig;
        private readonly SpawnPoseGeneratorService _spawnPoseGenerator;
        private readonly MonoBehaviour             _coroutineRunner;

        private          GameMode      _gameMode;
        private readonly MainCharacter _mainCharacter;

        public GameplayCycle(
            GameModeOptions           gameModeOptions,
            CharactersFactory         charactersFactory,
            MainCharacterConfig       mainCharacterConfig,
            Transform                 spawnPoint,
            EnemyCharacterConfig      enemyCharacterConfig,
            LevelConfig               levelConfig,
            SpawnPoseGeneratorService spawnPoseGenerator,
            MonoBehaviour             coroutineRunner)
        {
            _gameModeOptions = gameModeOptions;
            _charactersFactory = charactersFactory;
            _levelConfig = levelConfig;
            _enemyCharacterConfig = enemyCharacterConfig;
            _spawnPoseGenerator = spawnPoseGenerator;
            _coroutineRunner = coroutineRunner;

            _mainCharacter = _charactersFactory.CreateMainCharacter(mainCharacterConfig, spawnPoint);
        }

        public void Dispose()
        {
            ProcessFinish();
        }

        public IEnumerator Launch()
        {
            // Confirm pop up window here

            _gameMode = new GameMode(_gameModeOptions, _levelConfig, _charactersFactory, _enemyCharacterConfig, _spawnPoseGenerator, _mainCharacter);

            _gameMode.Win.Changed += OnGameModeWin;
            _gameMode.Defeat.Changed += OnGameModeDefeat;

            _gameMode.Start();

            yield return null;
        }

        public void Update(float deltaTime)
        {
            _gameMode?.Update(deltaTime);
        }

        private void OnGameModeWin(bool b, bool b1)
        {
            ProcessFinish();
            Debug.Log("Win!");
            _coroutineRunner.StartCoroutine(Launch());
        }

        private void OnGameModeDefeat(bool b, bool b1)
        {
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