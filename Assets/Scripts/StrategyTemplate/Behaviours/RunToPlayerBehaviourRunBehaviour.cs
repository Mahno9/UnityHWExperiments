using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunToPlayerBehaviourRunBehaviour : RunBehaviourBase
    {
        public RunToPlayerBehaviourRunBehaviour(Transform owner, Player player) : base(owner, player)
        {
        }

        protected override Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition)
        {
            return (playerPosition - owner.position).normalized;
        }
    }
}