using Navigation.Damage.Behaviours;
using Navigation.Damage.Interfaces;
using Navigation.ObjectsFacades;

using UnityEngine;
using UnityEngine.Assertions;

namespace Navigation.FX.Behaviours
{
    [RequireComponent(typeof(Health))]
    public class CharacterView : MonoBehaviour, IHealthChangeSubscriber, IExplosionTrigger
    {
        private const float Epsilon = 0.05f;

        private const string InjuredLayerName = "Injured";
        private const float  MaxLayerWeight   = 1;

        [SerializeField] private Animator _animator;
        [SerializeField] private float    _injureHealth = 30;
        private readonly         int      _damagedKey   = Animator.StringToHash("Damaged");
        private readonly         int      _isDeadKey    = Animator.StringToHash("IsDead");

        private readonly int _isRunningKey = Animator.StringToHash("IsRunning");

        private Character _character;
        private Health    _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.SubscribeOnHealthChange(this);
        }

        private void Update()
        {
            Assert.IsNotNull(_animator);

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
        }

        private void UpdateIsRunning()
        {
            bool isRunning = _character.MoveSpeed > Epsilon;
            _animator.SetBool(_isRunningKey, isRunning);
        }

        private void UpdateDamaged()
        {
            if (_health.IsDead())
                _animator.SetBool(_isDeadKey, true);
        }

        private void UpdateInjuredState()
        {
            if (_health.RemainHealth <= _injureHealth)
                _animator.SetLayerWeight(_animator.GetLayerIndex(InjuredLayerName), MaxLayerWeight);
        }
    }
}