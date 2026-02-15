using System.Collections.Generic;

using Navigation.CoreMechanics.Health.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Health
{
    public class Health : IHealthChangeBroadcaster
    {
        private const float MinHealthEpsilon = 0.001f;

        public float MaxHealth { get; }

        private readonly List<IHealthChangeSubscriber> _damageSubscribers = new();

        public Health(float maxHealth)
        {
            MaxHealth = maxHealth;
            RemainHealth = MaxHealth;
        }

        public float RemainHealth { get; private set; }

        public void TakeDamage(float damage)
        {
            float damageAdjusted = Mathf.Min(RemainHealth, damage);
            RemainHealth -= damageAdjusted;

            NotifySubscribersDamage(damageAdjusted);
        }

        public void Heal(float healthPoints)
        {
            float healthPointsAdjusted = Mathf.Min(MaxHealth - RemainHealth, healthPoints);
            RemainHealth += healthPointsAdjusted;

            NotifySubscribersHeal(healthPointsAdjusted);
        }

        public bool IsDead()
        {
            return Mathf.Abs(RemainHealth) < MinHealthEpsilon;
        }

        public void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber)
        {
            _damageSubscribers.Add(subscriber);
        }

        private void NotifySubscribersDamage(float damage)
        {
            foreach (IHealthChangeSubscriber damageSubscriber in _damageSubscribers)
                damageSubscriber.DamageTaken(damage);
        }

        private void NotifySubscribersHeal(float healthPoints)
        {
            foreach (IHealthChangeSubscriber damageSubscriber in _damageSubscribers)
                damageSubscriber.HealTaken(healthPoints);
        }
    }
}