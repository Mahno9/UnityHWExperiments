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

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);

        public bool IsInJumpProcess { get; }

        void SetMovePoint(Vector3 point);

        void        Jump(OffMeshLinkData offMeshLinkData);

    }
}