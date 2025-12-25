using UnityEngine;

namespace Navigation.CoreMechanics.Movement.Interfaces
{
    public interface IMover
    {
        public Vector3 MoveDirection { get; }
    }
}