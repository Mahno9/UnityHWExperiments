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
        private readonly GameMode      _gameMode;
        private readonly MonoBehaviour _coroutineRunner;

        public GameplayCycle(
            GameMode      gameMode,
            MonoBehaviour coroutineRunner)
        {
            _gameMode = gameMode;
            _coroutineRunner = coroutineRunner;
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