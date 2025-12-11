using UnityEngine;

namespace InteractionExample.PointerInteractions.Logic
{
    public class ExplosionShooterLogic
    {
        private readonly float _explosionRadius;
        private readonly float _explosionForce;

        public ExplosionShooterLogic(float explosionRadius, float explosionForce)
        {
            _explosionRadius = explosionRadius;
            _explosionForce = explosionForce;
        }

        public bool Shoot(Ray ray, out Vector3 explosionPoint)
        {
            RaycastHit[] hits = DraggableHits.GetHitsByRaySorted(ray);

            if (hits.Length == 0)
            {
                explosionPoint = Vector3.zero;
                return false;
            }

            explosionPoint = hits[0].point;

            Collider[] exploidItems = Physics.OverlapSphere(explosionPoint, _explosionRadius);
            Debug.Log($"Explosion pos: {explosionPoint}, items: {exploidItems.Length}");

            foreach (Collider item in exploidItems)
            {
                if (item.TryGetComponent(out Rigidbody rb))
                    rb.AddExplosionForce(_explosionForce, explosionPoint, _explosionRadius);
            }

            return true;
        }
    }
}