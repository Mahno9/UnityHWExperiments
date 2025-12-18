using UnityEngine;

namespace Navigation.Interfaces
{
    public interface IRotatable : IUpdatable
    {
        float RotationSpeed { get; }

        void SetLookDirection(Vector3 direction);
    }
}