using Navigation.Interfaces;

using UnityEngine;

namespace Navigation.Damage.Behaviours
{
    [RequireComponent(typeof(Health))]
    public class DestroyOnDie : MonoBehaviour, IHealthChangeSubscriber
    {
        private const            float      MinHealthEpsilon = 0.001f;
        [SerializeField] private float      _health;
        [SerializeField] private GameObject _destructionEffectPrefab;

        private void Awake()
        {
            Health healthComponent = GetComponent<Health>();
            healthComponent.SubscribeOnHealthChange(this);
        }

        public void DamageTaken(float damage)
        {
            _health -= Mathf.Min(damage, _health);

            if (_health <= MinHealthEpsilon)
                DestroySelf();
        }

        private void DestroySelf()
        {
            Instantiate(_destructionEffectPrefab, transform.position, _destructionEffectPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}