using System;

using Navigation.Controllers;
using Navigation.Utils;

using UnityEngine;

namespace Delegates.Timer
{
    public class TimerService : IUpdatable
    {
        private          float                   _timeTotal;
        private readonly ReactiveVariable<float> _timeLeft = new();

        public event Action<float> OnTimerStarted; // TimeTotal

        public IReactiveVariableReadonly<float> OnTimerUpdated => _timeLeft;

        public event Action OnTimerStopped;

        public bool IsStarted { get; private set; }

        public void Update(float deltaTime)
        {
            if (IsStarted == false)
                return;

            _timeLeft.Value -= Mathf.Min(deltaTime, _timeLeft.Value);

            if (_timeLeft.Value <= 0)
                StopTimer();
        }

        public void StartTimer(float time, bool forceRestart = false)
        {
            if (IsStarted && !forceRestart)
                return;

            _timeTotal = time;
            _timeLeft.Value = time;

            OnTimerStarted?.Invoke(_timeTotal);

            IsStarted = true;
        }

        public void StopTimer()
        {
            _timeLeft.Value = 0;
            IsStarted = false;
            OnTimerStopped?.Invoke();
        }

        public void PauseTimer() => IsStarted = false;

        public void ResumeTimer() => IsStarted = true;
    }
}