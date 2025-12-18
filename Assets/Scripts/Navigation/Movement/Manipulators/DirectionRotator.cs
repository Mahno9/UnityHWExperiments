using Navigation.Interfaces;

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Navigation.Manipulators
{
    public class DirectionRotator : IRotatable
    {
        private const float Epsilon = 0.05f;

        private readonly Transform _transform;
        private          Vector3   _currentLookDirection;

        public float RotationSpeed { get; }

        public DirectionRotator(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            RotationSpeed = rotationSpeed;
        }

        public void SetLookDirection(Vector3 newDirection) => _currentLookDirection = newDirection;

        public void Update(float deltaTime)
        {
            if (_currentLookDirection.magnitude < Epsilon)
                return;

            Quaternion toRotation = Quaternion.LookRotation(_currentLookDirection);
            float step = RotationSpeed * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, step);
        }
    }
}