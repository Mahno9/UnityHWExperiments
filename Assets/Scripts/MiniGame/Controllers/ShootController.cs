using MiniGame.CoreMechanics.Shooting;

using Navigation.Controllers;

using UnityEngine;

namespace MiniGame.Characters
{
    public class ShootController : ControllerBase
    {
        private readonly IShooter _shooter;

        public ShootController(IShooter shooter)
        {
            _shooter = shooter;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                _shooter.Shoot();
        }
    }
}