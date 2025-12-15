using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Controllers
{
    public abstract class ControllerBase : IUpdatable
    {
        private bool _isEnabled;

        public virtual void Enable() => _isEnabled = true;
        public virtual void Disabled() => _isEnabled = false;

        public void Update(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }

        protected abstract void UpdateLogic(float deltaTime);
    }
}