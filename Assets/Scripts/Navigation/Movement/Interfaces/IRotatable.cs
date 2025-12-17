using UnityEngine;

namespace Navigation.Interfaces
{
    public interface IRotatable : IUpdatable
    {
        float RotationSpeed { get; }

        void SetLookPoint(Vector3 direction);
    }
}