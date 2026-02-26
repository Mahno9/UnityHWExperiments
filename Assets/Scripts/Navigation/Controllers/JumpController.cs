using Navigation.Characters.Interfaces;

using UnityEngine.AI;

namespace Navigation.Controllers
{
    public class JumpController : ControllerBase
    {
        private readonly IJumpable _jumpable;

        public JumpController(IJumpable jumpable)
        {
            _jumpable = jumpable;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (!_jumpable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
                return;

            if (_jumpable.IsInJumpProcess == false)
                _jumpable.Jump(offMeshLinkData);
        }

        public bool IsJumping => _jumpable.IsInJumpProcess;
    }
}