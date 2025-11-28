using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunAwayBehaviourRunBehaviour : RunBehaviourBase
    {
        public RunAwayBehaviourRunBehaviour(Transform owner, Transform target) : base(owner, target)
        {
        }

        protected override Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition)
        {
            return (owner.position - playerPosition).normalized;
        }
    }
}