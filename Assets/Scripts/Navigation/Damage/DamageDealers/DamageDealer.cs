using System.Collections.Generic;

using Navigation.Damage.Behaviours;
using Navigation.Damage.Interfaces;
using Navigation.Interfaces;

namespace Navigation.Damage.DamageDealers
{
    public class DamageDealer : IDamageDealer
    {
        private readonly List<IDamageSubscriber> _subscribers = new();
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
            {
                target.TakeDamage(_damage);
                NotifySubscribers(target, _damage);
            }
        }

        public void SubscribeOnDamage(IDamageSubscriber subscriber) => _subscribers.Add(subscriber);

        private void NotifySubscribers(IDamageable target, float damage)
        {
            foreach (IDamageSubscriber subscriber in _subscribers)
                subscriber.DamageTaken(target, damage);
        }
    }
}