using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Health;
using Navigation.CoreMechanics.Health.Interfaces;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;
using Navigation.Heal;

using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Characters
{
    public class Character : MonoBehaviour, IMovable, IJumpable, IDamageable, IHealable, IDying, IHealthChangeBroadcaster
    {
        private Health                     _health;
        private NavMeshAgentMover          _mover;
        private AlongMoverDirectionRotator _rotator;

        public void Initialize(Health health, NavMeshAgentMover mover, AlongMoverDirectionRotator rotator)
        {
            _health = health;
            _mover = mover;
            _rotator = rotator;
        }

        public void Update()
        {
            Update(Time.deltaTime);
        }

        public void Update(float deltaTime)
        {
            _mover.Update(Time.deltaTime);
            _rotator.Update(Time.deltaTime);
        }

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public void Heal(float healthPoints) => _health.Heal(healthPoints);

        public float RemainHealth => _health.RemainHealth;

        public bool IsDead() => _health.IsDead();

        public float   MoveSpeed     => _mover.MoveSpeed;
        public Vector3 MoveDirection => _mover.MoveDirection;
        public Vector3 Position      => _mover.Position;

        public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData) => _mover.IsOnNavMeshLink(out offMeshLinkData);

        public bool IsInJumpProcess => _mover.IsInJumpProcess;

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);

        public void Jump(OffMeshLinkData offMeshLinkData) => _mover.Jump(offMeshLinkData);

        public void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber) => _health.SubscribeOnHealthChange(subscriber);
    }
}