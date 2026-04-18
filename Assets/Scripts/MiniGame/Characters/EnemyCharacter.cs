using System;

using Common.Utils;

using MiniGame.CoreMechanics.Damage;

using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;
using UnityEngine.AI;

using IDamageable = MiniGame.CoreMechanics.Damage.IDamageable;

namespace MiniGame.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyCharacter : MonoDestroyable, IMovable, IDamageable, IDying, IHaveHealth
    {
        public  IReactiveVariableReadonly<bool>  IsDead => _isDead;
        public  IReactiveVariableReadonly<float> Health => _health;

        public float   MoveSpeed => _mover.MoveSpeed;
        public Vector3 Position  => _mover.Position;

        private IDamageDealer                    _damageDealer;
        private ReactiveVariable<float>          _health;
        private ReactiveVariable<bool>           _isDead;
        private NavMeshAgentMover                _mover;

        private AlongMoverDirectionRotator _rotator;

        private void Awake()
        {
            enabled = false;
        }

        public void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                _damageDealer.Damage(damageable);
        }

        public void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
                _isDead.Value = true;
        }

        public TeamId GetTeamId()
        {
            return TeamId.Enemy;
        }

        public void SetMovePoint(Vector3 point)
        {
            _mover.SetMovePoint(point);
        }

        public void Initialize(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator, float startHealth, IDamageDealer damageDealer)
        {
            _mover = mover;
            _rotator = rotator;
            _health = new ReactiveVariable<float>(startHealth);
            _isDead = new ReactiveVariable<bool>(false);

            _damageDealer = damageDealer;

            enabled = true;
        }
    }
}