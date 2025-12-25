using Navigation.Controllers;
using Navigation.CoreMechanics.Movement.Interfaces;
using Navigation.CoreMechanics.Rotation.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Rotation
{
    public class AlongMoverDirectionRotator : IUpdatable
    {
        private readonly IMover     _mover;
        private readonly IRotatable _rotator;

        public AlongMoverDirectionRotator(IRotatable rotator, IMover mover)
        {
            _mover = mover;
            _rotator = rotator;
        }

        public void Update(float deltaTime)
        {
            Vector3 lookPoint = _mover.MoveDirection;

            _rotator.SetLookDirection(lookPoint.normalized);
            _rotator.Update(deltaTime);
        }
    }
}