using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public abstract class RunBehaviourBase : MovableBehaviourBase
    {
        private readonly Transform _target;

        protected RunBehaviourBase(Transform owner, Transform target) : base(owner)
        {
            _target = target;
        }

        public override void Update(float deltaTime)
        {
            var playerPosition = _target.transform.position;
            var directionNorm = CalcDirectionNorm(Owner, playerPosition);

            MoveTo(directionNorm);
        }

        protected abstract Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition);
    }
}