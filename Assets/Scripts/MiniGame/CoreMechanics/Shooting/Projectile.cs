using System.Linq;

using Navigation.Characters.Interfaces;

using UnityEngine;

namespace MiniGame.CoreMechanics.Shooting
{
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _maxDistance = 100f;

        private IDamageable[] _friends;
        private float         _flightDistance;


        public void Initialize(params IDamageable[] friends)
        {
            _friends = friends;
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
            if (!other.TryGetComponent(out IDamageable target))
            {
                Destroy(gameObject);
                return;
            }

            if (IsFriend(target))
                return;

            target.TakeDamage(_damage);
            Destroy(gameObject);
        }

        private bool IsFriend(IDamageable target) => _friends.Any(friend => friend == target);
    }
}