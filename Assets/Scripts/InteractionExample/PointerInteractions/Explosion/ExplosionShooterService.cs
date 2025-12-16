using InteractionExample.PointerInteractions.Explosion.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.Explosion
{
    public class ExplosionShooterService
    {
        private readonly float _explosionForce;
        private readonly float _explosionRadius;

        public ExplosionShooterService(float explosionRadius, float explosionForce)
        {
            _explosionRadius = explosionRadius;
            _explosionForce = explosionForce;
        }

        public bool Shoot(Ray ray, out Vector3 explosionPoint)
        {
            if (Physics.Raycast(ray, out RaycastHit hit) == false)
            {
                explosionPoint = Vector3.zero;
                return false;
            }

            explosionPoint = hit.point;

            Collider[] exploidItems = Physics.OverlapSphere(explosionPoint, _explosionRadius);
            Debug.Log($"Explosion pos: {explosionPoint}, items: {exploidItems.Length}");

            foreach (Collider item in exploidItems)
                if (item.TryGetComponent(out IForceable fItem))
                    fItem.AddExplosionForce(explosionPoint, _explosionForce, _explosionRadius);

            return true;
        }
    }
}