using Navigation.Controllers;

using UnityEngine.AI;

namespace Navigation.Characters.Interfaces
{
    public interface IJumpable : IUpdatable
    {
        public bool IsInJumpProcess { get; }
        void        Jump(OffMeshLinkData offMeshLinkData);

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData);
    }
}