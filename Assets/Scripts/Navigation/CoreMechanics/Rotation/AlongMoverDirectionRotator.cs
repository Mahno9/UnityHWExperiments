using Navigation.Controllers;
using Navigation.CoreMechanics.Movement.Interfaces;
using Navigation.CoreMechanics.Rotation.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Rotation
{
    public class AlongMoverDirectionRotator : DirectionRotator
    {
        private readonly IMover     _mover;

        public AlongMoverDirectionRotator(Transform transform, float rotationSpeed, IMover mover)
            : base(transform, rotationSpeed)
        {
            _mover = mover;
        }

        public override void Update(float deltaTime)
        {
            Vector3 lookPoint = _mover.MoveDirection;
            SetLookDirection(lookPoint.normalized);
            base.Update(deltaTime);
        }
    }
}