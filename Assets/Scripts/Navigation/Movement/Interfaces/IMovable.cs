using UnityEngine;

namespace Navigation.Interfaces
{
    public interface IMovable : IUpdatable
    {
        public float   MoveSpeed { get; }

        public Vector3 MovePoint { get; }

        public Vector3 Position { get; }

        void SetMovePoint(Vector3 point);
    }
}