using UnityEngine;

namespace Navigation.Movement.Interfaces
{
    public interface IMover
    {
        public Vector3 MoveDirection { get; }
    }
}