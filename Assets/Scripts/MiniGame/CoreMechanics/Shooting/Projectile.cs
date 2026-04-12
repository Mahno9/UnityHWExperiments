using System.Linq;

using MiniGame.CoreMechanics.Damage;

using UnityEngine;

namespace MiniGame.CoreMechanics.Shooting
{
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxDistance = 100f;

        // private TeamId        _shooterTeamId;
        private float         _flightDistance;
        private IDamageDealer _damageDealer;


        public void Initialize(IDamageDealer damageDealer)
        {
            _damageDealer = damageDealer;
            // _shooterTeamId = teamId;
        }

        private void Update()
        {
            Vector3 projectileShift = transform.forward * (_speed * Time.deltaTime);
            transform.position += projectileShift;

            ProcessProjectileSpent(projectileShift.sqrMagnitude);
        }

        private void ProcessProjectileSpent(float sqrDistance)
        {
            // Optimization ༼ つ ◕_◕ ༽つ
            _flightDistance += sqrDistance;
            if (_flightDistance >= _maxDistance * _maxDistance)
                Destroy(gameObject);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable target) || _damageDealer.Damage(target))
                Destroy(gameObject);
        }
    }
}