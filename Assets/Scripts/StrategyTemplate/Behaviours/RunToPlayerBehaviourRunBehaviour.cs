using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunToPlayerBehaviourRunBehaviour : RunBehaviourBase
    {
        public RunToPlayerBehaviourRunBehaviour(Transform owner, Transform target) : base(owner, target)
        {
        }

        protected override Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition)
        {
            return (playerPosition - owner.position).normalized;
        }
    }
}