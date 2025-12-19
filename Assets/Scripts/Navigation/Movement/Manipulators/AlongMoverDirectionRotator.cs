using Navigation.Common.Interfaces;
using Navigation.Movement.Interfaces;

using UnityEngine;

namespace Navigation.Movement.Manipulators
{
    public class AlongMoverDirectionRotator : IUpdatable
    {
        private readonly IMovable   _mover;
        private readonly IRotatable _rotator;

        public AlongMoverDirectionRotator(IMovable mover, IRotatable rotator)
        {
            _mover = mover;
            _rotator = rotator;
        }

        public float RotationSpeed => _rotator.RotationSpeed;

        public void Update(float deltaTime)
        {
            Vector3 lookPoint = _mover.MoveDirection;

            _rotator.SetLookDirection(lookPoint.normalized);
            _rotator.Update(deltaTime);
        }
    }
}