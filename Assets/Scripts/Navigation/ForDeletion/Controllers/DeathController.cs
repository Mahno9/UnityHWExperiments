using Navigation.Damage.Interfaces;
using Navigation.Movement.Controllers;

namespace Navigation.Common.Controllers
{
    public class DeathController : IHealthChangeSubscriber
    {
        private readonly IHealth          _health;
        private readonly ControllerBase[] _onlyAliveControllers;

        public DeathController(IHealth health, params ControllerBase[] onlyAliveControllers)
        {
            _health = health;
            _onlyAliveControllers = onlyAliveControllers;

            _health.SubscribeOnHealthChange(this);
        }

        public void DamageTaken(float damage)
        {
            if (!_health.IsDead())
                return;

            foreach (ControllerBase controller in _onlyAliveControllers)
                controller.Disable();
        }
    }
}