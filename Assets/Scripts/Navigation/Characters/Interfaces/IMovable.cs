using Navigation.Controllers;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Characters.Interfaces
{
    public interface IMovable : IUpdatable
    {
        public float MoveSpeed { get; }

        public Vector3 MoveDirection { get; }

        public Vector3 Position { get; }

        void SetMovePoint(Vector3 point);
    }

    public interface IJumpable : IUpdatable
    {
        public bool IsInJumpProcess { get; }
        void        Jump(OffMeshLinkData offMeshLinkData);

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);
    }
}