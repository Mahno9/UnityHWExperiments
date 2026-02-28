using System;

using Navigation.Controllers;

using UnityEngine;

namespace Navigation.Timer
{
    public class TimerService : IUpdatable
    {
        private float _timeTotal;
        private float _timeLeft;

        private bool  _isStarted;

        public event Action<float, float> OnTimerTicked; // TimeTotal, TimeLeft

        public void Update(float deltaTime)
        {
            if (_isStarted == false)
                return;

            _timeLeft -= Mathf.Min(deltaTime, _timeLeft);

            OnTimerTicked?.Invoke(_timeTotal, _timeLeft);

            if (_timeLeft <= 0)
                _isStarted = false;
        }

        public void StartTimer(float time)
        {
            if (_isStarted)
                return;

            _timeTotal = time;
            _timeLeft = time;

            OnTimerTicked?.Invoke(_timeTotal, _timeLeft);

            _isStarted = true;
        }
    }
}