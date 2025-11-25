using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class PatrolBrownianBehaviour : IUpdatableBehaviour
    {
        private const float DIRECTION_PICK_INTERVAL = 1f;
        private readonly float _moveSpeed;
        private Vector3 _currentDirection;

        private float _timeUntilNextPick;

        public PatrolBrownianBehaviour(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Update(float deltaTime, Transform owner)
        {
            _timeUntilNextPick -= deltaTime;
            TryUpdateDirection();
            owner.position += _currentDirection * (_moveSpeed * deltaTime);
        }

        private void TryUpdateDirection()
        {
            if (_timeUntilNextPick > 0)
                return;

            _currentDirection = PickNewDirection();
            _timeUntilNextPick = DIRECTION_PICK_INTERVAL;
        }

        private static Vector3 PickNewDirection()
        {
            var newDirection = Random.insideUnitSphere;
            newDirection.y = 0;
            return newDirection;
        }
    }
}