using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunAwayBehaviour : BaseRunBehaviour
    {
        public RunAwayBehaviour(Player player, float moveSpeed) : base(player, moveSpeed)
        {
        }

        protected override Vector3 CalcDirection(Transform owner, Vector3 playerPosition)
        {
            return (owner.position - playerPosition).normalized;
        }
    }
}