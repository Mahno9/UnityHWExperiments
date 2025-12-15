using UnityEngine;

namespace Navigation.Interfaces
{
    public interface IMovable : IUpdatable
    {
        public float   MoveSpeed { get; }

        public Vector3 MoveDirection { get; }

        public Vector3 Position { get; }

        void SetTargetPosition(Vector3 position);
    }
}