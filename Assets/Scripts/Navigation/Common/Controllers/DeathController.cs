using System.Collections.Generic;

using Navigation.Controllers;
using Navigation.Damage.Behaviours;
using Navigation.Interfaces;

namespace Navigation.Common.Controllers
{
    public class DeathController : IDamageSubscriber
    {
        private readonly ControllerBase[] _onlyAliveControllers;
        private readonly IDamageable      _health;

        public DeathController(IDamageable health, params ControllerBase[] onlyAliveControllers)
        {
            _health = health;
            _onlyAliveControllers = onlyAliveControllers;

            _health.SubscribeOnDamage(this);
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