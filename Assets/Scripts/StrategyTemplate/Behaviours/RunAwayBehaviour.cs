using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunAwayBehaviourBase : RunBehaviourBase
    {
        public RunAwayBehaviourBase(Transform owner, Transform target) : base(owner, target)
        {
        }

        protected override Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition)
        {
            return (owner.position - playerPosition).normalized;
        }
    }
}