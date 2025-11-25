using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public abstract class BaseRunBehaviour : IUpdatableBehaviour
    {
        private readonly float _moveSpeed;
        private readonly Player _player;

        protected BaseRunBehaviour(Player player, float moveSpeed)
        {
            _player = player;
            _moveSpeed = moveSpeed;
        }

        public void Update(float deltaTime, Transform owner)
        {
            var playerPosition = _player.transform.position;
            var direction = CalcDirection(owner, playerPosition);

            owner.position += direction * (_moveSpeed * deltaTime);
        }

        protected abstract Vector3 CalcDirection(Transform owner, Vector3 playerPosition);
    }
}