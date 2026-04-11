using System;

using Common.Utils;

using MiniGame.CoreMechanics.Damage;
using MiniGame.CoreMechanics.Shooting;

using Navigation.Characters.Interfaces;
using Navigation.Controllers;
using Navigation.CoreMechanics.Rotation;
using Navigation.Utils;

using UnityEngine;

namespace MiniGame.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class MainCharacter : MonoDestroyable, ISimpleMovable, IRotatableInPosition, IDamageable, IDying, IShooter, IHaveHealth
    {
        [SerializeField] private Transform _muzzle;

        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        public Vector3 Position      => _characterController.transform.position;
        public float   RotationSpeed => _rotator.RotationSpeed;

        private ReactiveVariable<float> _health;
        private ReactiveVariable<bool>  _isDead;
        private CharacterController     _characterController;
        private DirectionRotator        _rotator;
        private Shooter                 _shooter;

        private void Awake() => enabled = false;

        public void Initialize(CharacterController characterController, DirectionRotator rotator, float health, Shooter shooter)
        {
            _characterController = characterController;
            _rotator = rotator;
            _shooter = shooter;

            _health = new ReactiveVariable<float>(health);
            _isDead = new ReactiveVariable<bool>(false);

            enabled = true;
        }

        public void Update()
        {
            _rotator.Update(Time.deltaTime);
        }

        public void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            Debug.Log($"Damage: {damage}");
            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
                _isDead.Value = true;
        }

        public void Move(Vector3 direction) => _characterController.Move(direction);

        public void SetLookDirection(Vector3 direction) => _rotator.SetLookDirection(direction);

        public void Shoot() => _shooter.Shoot(_muzzle, this);
    }
}