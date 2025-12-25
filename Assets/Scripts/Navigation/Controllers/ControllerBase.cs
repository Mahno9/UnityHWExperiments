namespace Navigation.Controllers
{
    public abstract class ControllerBase : IUpdatable
    {
        private bool _isEnabled;

        public void Update(float deltaTime)
        {
            if (_isEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }

        public virtual void Enable()
        {
            _isEnabled = true;
        }

        public virtual void Disable()
        {
            _isEnabled = false;
        }

        protected abstract void UpdateLogic(float deltaTime);
    }
}