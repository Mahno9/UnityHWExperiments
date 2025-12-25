using Navigation.Common.Interfaces;

using UnityEngine;

namespace Navigation.Movement.Interfaces
{
    public interface IRotatable : IUpdatable
    {
        float RotationSpeed { get; }

        void SetLookDirection(Vector3 direction);
    }
}