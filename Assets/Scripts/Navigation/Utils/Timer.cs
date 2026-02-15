using System;

using Navigation.Controllers;

namespace Navigation.Utils
{
    public class Timer : IUpdatable
    {
        private readonly Action _action;
        private          float  _remainTime;

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