using Navigation.Common.Interfaces;

using UnityEngine;

namespace Navigation.Movement.Interfaces
{
    public interface IMovable : IUpdatable
    {
        public float MoveSpeed { get; }

        public Vector3 MoveDirection { get; }

        public Vector3 Position { get; }

        void SetMovePoint(Vector3 point);
    }
}