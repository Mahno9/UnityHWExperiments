using System;

using Common.Utils;

using MiniGame.CoreMechanics.Damage;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
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
        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        public float   MoveSpeed => _mover.MoveSpeed;
        public Vector3 Position  => _mover.Position;

        private AlongMoverDirectionRotator _rotator;
        private NavMeshAgentMover          _mover;
        private ReactiveVariable<float>    _health;
        private ReactiveVariable<bool>     _isDead;

        private void Awake() => enabled = false;

        public void Initialize(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator, float startHealth)
        {
            _mover   = mover;
            _rotator = rotator;
            _health  = new ReactiveVariable<float>(startHealth);
            _isDead  = new ReactiveVariable<bool>(false);

            enabled  = true;
        }

        public void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        public void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
            {
                _isDead.Value = true;
                Destroy();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"Collided with: {other.gameObject.name}");
        }

        public TeamId GetTeamId() => TeamId.Enemy;

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);
    }
}
