using System;

using Navigation.Interfaces;

namespace Navigation.Common
{
    public class Timer : IUpdatable
    {
        private          float  _remainTime;
        private readonly Action _action;

        public Timer(float time, Action action)
        {
            _remainTime = time;
            _action = action;
        }

        public void Update(float deltaTime)
        {
            _remainTime -= deltaTime;

            if (_remainTime <= 0)
                _action();
        }
    }
}