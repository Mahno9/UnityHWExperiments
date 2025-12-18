using System;

using Navigation.Controllers;
using Navigation.Damage.Behaviours;
using Navigation.Interfaces;

using UnityEngine;
using UnityEngine.Assertions;

namespace Navigation.Behaviours
{
    [RequireComponent(typeof(IDamageable))]
    public class CharacterView : MonoBehaviour, IDamageSubscriber
    {
        private const float Epsilon = 0.05f;

        private const string InjuredLayerName = "Injured";
        private const float  MaxLayerWeight   = 1;

        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");
        private readonly int _damagedKey   = Animator.StringToHash("Damaged");
        private readonly int _isDeadKey    = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;
        [SerializeField] private float    _injureHealth = 30;

        private MoveController _moveController;
        private IDamageable    _health;

        private void Awake()
        {
            _health = GetComponent<IDamageable>();
            _health.SubscribeOnDamage(this);
        }

        public void SetMoveController(MoveController moveController)
        {
            _moveController = moveController;
        }

        private void Update()
        {
            Assert.IsNotNull(_animator);

            UpdateIsRunning();
            UpdateDamaged();
        }

        private void UpdateIsRunning()
        {
            bool isRunning = _moveController.MoveSpeed > Epsilon;
            _animator.SetBool(_isRunningKey, isRunning);
        }

        private void UpdateDamaged()
        {
            if (_health.IsDead())
                _animator.SetBool(_isDeadKey, true);
        }

        public void DamageTaken(float damage)
        {
            _animator.SetTrigger(_damagedKey);
            UpdateInjuredState();
        }

        private void UpdateInjuredState()
        {
            if (_health.RemainHealth <= _injureHealth)
                _animator.SetLayerWeight(_animator.GetLayerIndex(InjuredLayerName), MaxLayerWeight);
        }
    }
}