using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public abstract class RunBehaviourBase : MovableBehaviourBase
    {
        private readonly Player _player;

        protected RunBehaviourBase(Transform owner, Player player) : base(owner)
        {
            _player = player;
        }

        public override void Update(float deltaTime)
        {
            var playerPosition = _player.transform.position;
            var directionNorm = CalcDirectionNorm(Owner, playerPosition);

            MoveTo(directionNorm);
        }

        protected abstract Vector3 CalcDirectionNorm(Transform owner, Vector3 playerPosition);
    }
}