using MiniGame.CoreMechanics.Damage;

using UnityEngine;

namespace MiniGame.CoreMechanics.Shooting
{
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float         _speed;
        [SerializeField] private float         _damage;
        [SerializeField] private float         _maxDistance = 100f;
        private                  IDamageDealer _damageDealer;

        private float _flightDistance;

        public void Initialize(IDamageDealer damageDealer)
        {
            _damageDealer = damageDealer;
        }

        private void Update()
        {
            Vector3 projectileShift = transform.forward * (_speed * Time.deltaTime);
            transform.position += projectileShift;

            ProcessProjectileSpent(projectileShift.sqrMagnitude);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable target) || _damageDealer.Damage(target))
                Destroy(gameObject);
        }

        private void ProcessProjectileSpent(float sqrDistance)
        {
            // Optimization ༼ つ ◕_◕ ༽つ
            _flightDistance += sqrDistance;
            if (_flightDistance >= _maxDistance * _maxDistance)
                Destroy(gameObject);
        }
    }
}