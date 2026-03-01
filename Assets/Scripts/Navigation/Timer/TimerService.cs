using System;

using Navigation.Controllers;

using UnityEngine;

namespace Navigation.Timer
{
    public class TimerService : IUpdatable
    {
        private float _timeTotal;
        private float _timeLeft;

        public event Action<float, float> OnTimerUpdated; // TimeTotal, TimeLeft
        public event Action OnTimerStarted;
        public event Action OnTimerStopped;

        public bool IsStarted { get; private set; }

        public void Update(float deltaTime)
        {
            if (IsStarted == false)
                return;

            _timeLeft -= Mathf.Min(deltaTime, _timeLeft);

            OnTimerUpdated?.Invoke(_timeTotal, _timeLeft);

            if (_timeLeft <= 0)
                StopTimer();
        }

        public void StartTimer(float time, bool forceRestart = false)
        {
            if (IsStarted && !forceRestart)
                return;

            _timeTotal = time;
            _timeLeft = time;

            OnTimerStarted?.Invoke();
            OnTimerUpdated?.Invoke(_timeTotal, _timeLeft);

            IsStarted = true;
        }

        public void StopTimer()
        {
            _timeLeft = 0;
            IsStarted = false;
            OnTimerStopped?.Invoke();
        }

        public void PauseTimer() => IsStarted = false;

        public void ResumeTimer() => IsStarted = true;
    }
}