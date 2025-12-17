using Navigation.Interfaces;

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Navigation.Manipulators
{
    public class DirectionRotator : IRotatable
    {
        private const float Epsilon = 0.05f;

        private Transform _transform;
        private Vector3   _currentLookPoint;

        public float RotationSpeed { get; }

        public DirectionRotator(float rotationSpeed)
        {
            RotationSpeed = rotationSpeed;
        }

        public void SetLookPoint(Vector3 newDirection) => _currentLookPoint = newDirection;

        public void Update(float deltaTime)
        {
            if (_currentLookPoint.magnitude < Epsilon)
                return;

            Quaternion toRotation = Quaternion.LookRotation(_currentLookPoint.normalized);
            float step = RotationSpeed * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, step);
        }
    }
}