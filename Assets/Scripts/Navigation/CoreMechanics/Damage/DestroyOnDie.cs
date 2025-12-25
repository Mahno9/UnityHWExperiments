using Navigation.Characters.Interfaces;

using UnityEngine;

namespace Navigation.CoreMechanics.Damage
{
    public class DestroyOnDie : MonoBehaviour, IDamageable
    {
        [SerializeField] private float      _maxHealth;
        [SerializeField] private GameObject _destructionEffectPrefab;

        private Health.Health _health;

        private void Awake()
        {
            _health = new Health.Health(_maxHealth);
        }

        private void DestroySelf()
        {
            Instantiate(_destructionEffectPrefab, transform.position, _destructionEffectPrefab.transform.rotation);
            Destroy(gameObject);
        }

        public virtual void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);

            if (_health.IsDead())
                DestroySelf();
        }
    }
}