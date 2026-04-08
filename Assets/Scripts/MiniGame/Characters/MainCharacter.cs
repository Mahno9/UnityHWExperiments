using System;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;

namespace MiniGame.Characters
{
    public class MainCharacter : ControllersUpdater, ISimpleMovable, IRotatableInPosition, IDamageable, IDying
    {
        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        public Vector3 Position      => _characterController.transform.position;
        public float   RotationSpeed => _rotator.RotationSpeed;

        private readonly ReactiveVariable<float> _health;
        private readonly ReactiveVariable<bool>  _isDead;
        private readonly CharacterController     _characterController;
        private readonly DirectionRotator        _rotator;

        public MainCharacter(CharacterController characterController, DirectionRotator rotator, float health, params ControllerBase[] controllers) : base(controllers)
        {
            _characterController = characterController;
            _rotator             = rotator;
            _health              = new ReactiveVariable<float>(health);
            _isDead              = new ReactiveVariable<bool>(false);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            // _characterController updates just after move
            _rotator.Update(deltaTime);
        }

        public virtual void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
                _isDead.Value = true;
        }

        public void Move(Vector3 direction) => _characterController.Move(direction);

        public void SetLookDirection(Vector3 direction) => _rotator.SetLookDirection(direction);
    }
}
