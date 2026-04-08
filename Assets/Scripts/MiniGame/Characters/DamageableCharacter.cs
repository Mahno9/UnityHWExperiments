using System;
using Navigation.Utils;
using Navigation.Characters.Interfaces;
using Navigation.Controllers;

namespace MiniGame.Characters
{
    public abstract class DamageableCharacter : ControllersUpdater, IDamageable, IDying
    {
        public IReactiveVariableReadonly<float> Health => _health;
        public IReactiveVariableReadonly<bool>  IsDead => _isDead;

        private readonly ReactiveVariable<float> _health;
        private readonly ReactiveVariable<bool>  _isDead;

        protected DamageableCharacter(float startHealth, params ControllerBase[] controllers) : base(controllers)
        {
            _health = new ReactiveVariable<float>(startHealth);
            _isDead = new ReactiveVariable<bool>(false);
        }

        public virtual void TakeDamage(float damage)
        {
            if (_isDead.Value) return;

            _health.Value = MathF.Max(0, _health.Value - damage);
            if (_health.Value <= 0)
                _isDead.Value = true;
        }
    }
}
