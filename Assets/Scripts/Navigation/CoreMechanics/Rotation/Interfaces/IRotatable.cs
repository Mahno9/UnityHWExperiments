using Navigation.Controllers;

using UnityEngine;

namespace Navigation.CoreMechanics.Rotation.Interfaces
{
    public interface IRotatable
    {
        float RotationSpeed { get; }

        void SetLookDirection(Vector3 direction);
    }
}