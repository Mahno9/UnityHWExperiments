using System;

using Common.Utils;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;

namespace MiniGame.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacter : MonoDestroyable, ISimpleMovable, IRotatableInPosition, IDamageable, IDying
    {
        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        public Vector3 Position      => _characterController.transform.position;
        public float   RotationSpeed => _rotator.RotationSpeed;

        private ReactiveVariable<float> _health;
        private ReactiveVariable<bool>  _isDead;
        private CharacterController     _characterController;
        private DirectionRotator        _rotator;

        private void Awake() => enabled = false;

        public void Initialize(CharacterController characterController, DirectionRotator rotator, float health)
        {
            _characterController = characterController;
            _rotator             = rotator;
            _health              = new ReactiveVariable<float>(health);
            _isDead              = new ReactiveVariable<bool>(false);

            enabled = true;
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
                _isDead.Value = true;
        }

        public void Move(Vector3 direction) => _characterController.Move(direction);

        public void SetLookDirection(Vector3 direction) => _rotator.SetLookDirection(direction);
    }
}
