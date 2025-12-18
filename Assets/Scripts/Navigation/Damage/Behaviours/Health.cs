using System;
using System.Collections.Generic;

using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Damage.Behaviours
{
    public class Health : MonoBehaviour, IDamageable
    {
        private const float MinHealthEpsilon = 0.001f;

        [SerializeField] private float _maxHealth = 100;

        private readonly List<IHealthChangeSubscriber> _damageSubscribers = new();

        public float RemainHealth { get; private set; }

        private void Awake()
        {
            RemainHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            float damageAdjusted = Mathf.Min(RemainHealth, damage);
            RemainHealth -= damageAdjusted;

            NotifySubscribers(damageAdjusted);
        }

        public bool IsDead()
        {
            return Mathf.Abs(RemainHealth) < MinHealthEpsilon;
        }

        public void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber)
        {
            _damageSubscribers.Add(subscriber);
        }

        private void NotifySubscribers(float damage)
        {
            foreach (IHealthChangeSubscriber damageSubscriber in _damageSubscribers)
                damageSubscriber.DamageTaken(damage);
        }
    }
}