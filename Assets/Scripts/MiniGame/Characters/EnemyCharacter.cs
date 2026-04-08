using System;

using Delegates.Enemies.Controllers;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Movement;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;

namespace MiniGame.Characters
{
    public class EnemyCharacter : ControllersUpdater, IMovable, IDamageable, IDying
    {
        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        public float   MoveSpeed => _mover.MoveSpeed;
        public Vector3 Position  => _mover.Position;

        private readonly ReactiveVariable<float>    _health;
        private readonly ReactiveVariable<bool>     _isDead;
        private readonly AlongMoverDirectionRotator _rotator;
        private readonly NavMeshAgentMover          _mover;

        public EnemyCharacter(NavMeshAgentMover mover, AlongMoverDirectionRotator rotator, float startHealth, params ControllerBase[] controllers) : base(controllers)
        {
            _mover   = mover;
            _rotator = rotator;
            _health  = new ReactiveVariable<float>(startHealth);
            _isDead  = new ReactiveVariable<bool>(false);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _rotator.Update(deltaTime);
        }

        public virtual void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
                _isDead.Value = true;
        }

        public void SetMovePoint(Vector3 point) => _mover.SetMovePoint(point);
    }
}
