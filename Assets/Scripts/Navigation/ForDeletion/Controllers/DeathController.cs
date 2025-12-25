using System.Collections.Generic;

using Navigation.Characters.Interfaces;
using Navigation.Common.Interfaces;
using Navigation.Damage.Interfaces;
using Navigation.Movement.Controllers;

namespace Navigation.Common.Controllers
{
    public class DeathController : ControllerBase
    {
        private readonly IDying               _dying;
        private readonly List<ControllerBase> _onlyAliveControllers;

        public DeathController(IDying dying, params ControllerBase[] onlyAliveControllers)
        {
            _dying = dying;
            _onlyAliveControllers = new List<ControllerBase>(onlyAliveControllers) { this }; // TODO: check this
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (!_dying.IsDead())
                return;

            foreach (ControllerBase controller in _onlyAliveControllers)
                controller.Disable();
        }
    }
}