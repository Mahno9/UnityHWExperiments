using System;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;

using IDying = MiniGame.Characters.IDying;

namespace MiniGame.MainCharacter
{
    public class MainCharacter : IUpdatable, IMovable, IRotatableInPosition, IDamageable, IDying
    {
        public Vector3 Position      => _characterController.transform.position;
        public float   RotationSpeed => _rotator.RotationSpeed;

        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        private readonly CharacterController     _characterController;
        private readonly DirectionRotator        _rotator;
        private readonly ReactiveVariable<float> _health;
        private readonly ReactiveVariable<bool>  _isDead = new();

        public MainCharacter(CharacterController characterController, DirectionRotator rotator, float health)
        {
            _characterController = characterController;
            _rotator = rotator;
            _health = new ReactiveVariable<float>(health);
        }

        public void Update(float deltaTime)
        {
            // _characterController updates just after move
            _rotator.Update(deltaTime);
        }

        public void Move(Vector3 direction) => _characterController.Move(direction);

        public void SetLookDirection(Vector3 direction) => _rotator.SetLookDirection(direction);

        public void TakeDamage(float damage)
        {
            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value == 0)
                _isDead.Value = true;
        }
    }
}