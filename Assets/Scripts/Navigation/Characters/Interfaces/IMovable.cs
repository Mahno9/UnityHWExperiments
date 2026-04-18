using UnityEngine;

namespace Navigation.Characters.Interfaces
{
    public interface IMovable
    {
        public float MoveSpeed { get; }

        public Vector3 Position { get; }

        void SetMovePoint(Vector3 point);
    }
}