using Navigation.Characters.Interfaces;

using UnityEngine;

namespace Navigation.Controllers
{
    public class PointClickWithJumpsController : PointClickController
    {
        private readonly JumpController             _jumpController;

        public PointClickWithJumpsController(IMovable movable, JumpController jumpController, Camera camera, LayerMask groundLayerMask) : base(movable, camera, groundLayerMask)
        {
            _jumpController = jumpController;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            // Don't move in case of jumping
            if (_jumpController.IsJumping)
                return;

            base.UpdateLogic(deltaTime);
        }
    }
}