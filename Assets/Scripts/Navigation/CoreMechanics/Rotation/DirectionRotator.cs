using Navigation.CoreMechanics.Rotation.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Rotation
{
    public abstract class DirectionRotator
    {
        private const float Epsilon = 0.05f;

        private readonly Transform _transform;
        private          Vector3   _currentLookDirection;

        protected DirectionRotator(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            RotationSpeed = rotationSpeed;
        }

        public float RotationSpeed { get; }

        public void SetLookDirection(Vector3 newDirection)
        {
            _currentLookDirection = newDirection;
        }

        public virtual void Update(float deltaTime)
        {
            if (_currentLookDirection.magnitude < Epsilon)
                return;

            Quaternion toRotation = Quaternion.LookRotation(_currentLookDirection);
            float      step       = RotationSpeed * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, step);
        }
    }
}