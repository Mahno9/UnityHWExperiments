using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Health;
using Navigation.CoreMechanics.Health.Interfaces;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;

using UnityEngine;

namespace Navigation.Characters
{
    public class Character : IMovable, IDamageable, IDying, IHealthChangeBroadcaster
    {
        private readonly Health                     _health;
        private readonly NavMeshAgentMover          _mover;
        private readonly AlongMoverDirectionRotator _rotator;

        public Character(Health health, NavMeshAgentMover mover, AlongMoverDirectionRotator rotator)
        {
            _health = health;
            _mover = mover;
            _rotator = rotator;
        }

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public float RemainHealth => _health.RemainHealth;

        public bool IsDead() => _health.IsDead();

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);

        public void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber) => _health.SubscribeOnHealthChange(subscriber);

        public void Update(float deltaTime)
        {
            _mover.Update(deltaTime);
            _rotator.Update(deltaTime);
        }
    }
}