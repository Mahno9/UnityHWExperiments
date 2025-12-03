using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class PatrolBrownianBehaviourBase : MovableBehaviourBase
    {
        private const float DIRECTION_PICK_INTERVAL = 1f;
        private Vector3 _currentDirectionNorm;

        private float _timeUntilNextPick;

        public PatrolBrownianBehaviourBase(Transform owner) : base(owner)
        {
        }

        public override void Update(float deltaTime)
        {
            _timeUntilNextPick -= deltaTime;
            TryUpdateDirection();

            MoveTo(_currentDirectionNorm);
        }

        private void TryUpdateDirection()
        {
            if (_timeUntilNextPick > 0)
                return;

            _currentDirectionNorm = PickNewDirectionNorm();
            _timeUntilNextPick = DIRECTION_PICK_INTERVAL;
        }

        private static Vector3 PickNewDirectionNorm()
        {
            var newDirection = Random.insideUnitSphere;
            newDirection.y = 0;
            return newDirection.normalized;
        }
    }
}