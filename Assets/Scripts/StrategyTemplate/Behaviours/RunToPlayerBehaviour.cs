using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class RunToPlayerBehaviourBase : RunBehaviourBase
    {
        public RunToPlayerBehaviourBase(Transform owner, Transform target) : base(owner, target)
        {
        }

        protected override Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition)
        {
            return (playerPosition - owner.position).normalized;
        }
    }
}