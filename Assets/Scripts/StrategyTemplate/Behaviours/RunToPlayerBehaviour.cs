using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunToPlayerBehaviour : BaseRunBehaviour
    {
        public RunToPlayerBehaviour(Player player, float moveSpeed) : base(player, moveSpeed)
        {
        }

        protected override Vector3 CalcDirection(Transform owner, Vector3 playerPosition)
        {
            return (playerPosition - owner.position).normalized;
        }
    }
}