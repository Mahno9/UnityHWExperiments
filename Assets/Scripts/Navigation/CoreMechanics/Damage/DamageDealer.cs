using Navigation.Characters.Interfaces;
using Navigation.CoreMechanics.Damage.Interfaces;

namespace Navigation.CoreMechanics.Damage
{
    public class DamageDealer
    {
        private readonly float                   _damage;

        private readonly ITargetsDetector _targetsDetector;

        public DamageDealer(float damage, ITargetsDetector targetDetector)
        {
            _damage = damage;
            _targetsDetector = targetDetector;
        }

        public void DealDamage()
        {
            IDamageable[] targets = _targetsDetector.GetTargets();
            foreach (IDamageable target in targets)
                target.TakeDamage(_damage);
        }
    }
}