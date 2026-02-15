using Navigation.CoreMechanics.Health;
using Navigation.CoreMechanics.Health.Interfaces;

using UnityEngine;
using UnityEngine.Assertions;

namespace Navigation.Characters
{
    public class CharacterView : MonoBehaviour, IHealthChangeSubscriber
    {
        private const float Epsilon = 0.05f;

        private const string InjuredLayerName = "Injured";
        private const float  MaxLayerWeight   = 1;

        [SerializeField] private Animator _animator;
        [SerializeField] private float    _injureHealth       = 30;
        private readonly         int      _damagedKey         = Animator.StringToHash("Damaged");
        private readonly         int      _isDeadKey          = Animator.StringToHash("IsDead");
        private readonly         int      _isInJumpProcessKey = Animator.StringToHash("IsInJumpProcess");

        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");

        private Character _character;

        private void Update()
        {
            Assert.IsNotNull(_animator);

            UpdateJumping();
            UpdateIsRunning();
            UpdateDamaged();
        }

        public void DamageTaken(float damage)
        {
            _animator.SetTrigger(_damagedKey);
            UpdateInjuredState();
        }

        public void SetCharacter(Character character)
        {
            _character = character;
            _character.SubscribeOnHealthChange(this);
        }

        private void UpdateJumping()
        {
            _animator.SetBool(_isInJumpProcessKey, _character.IsInJumpProcess);
        }

        private void UpdateIsRunning()
        {
            bool isRunning = _character.MoveSpeed > Epsilon;
            _animator.SetBool(_isRunningKey, isRunning);
        }

        private void UpdateDamaged()
        {
            if (_character.IsDead())
                _animator.SetBool(_isDeadKey, true);
        }

        private void UpdateInjuredState()
        {
            if (_character.RemainHealth <= _injureHealth)
                _animator.SetLayerWeight(_animator.GetLayerIndex(InjuredLayerName), MaxLayerWeight);
        }
    }
}