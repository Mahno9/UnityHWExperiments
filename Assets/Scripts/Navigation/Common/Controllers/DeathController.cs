using System.Collections.Generic;

using Navigation.Controllers;
using Navigation.Damage.Behaviours;
using Navigation.Interfaces;

namespace Navigation.Common.Controllers
{
    public class DeathController : IDamageSubscriber
    {
        private readonly ControllerBase[] _onlyAliveControllers;

        public DeathController(IDamageable health, params ControllerBase[] onlyAliveControllers)
        {
            _onlyAliveControllers = onlyAliveControllers;
            health.SubscribeOnDamage(this);
        }

        public void DamageTaken(float damage)
        {
            foreach (ControllerBase controller in _onlyAliveControllers)
                controller.Disable();
        }
    }
}