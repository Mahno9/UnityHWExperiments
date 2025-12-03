using UnityEngine;

namespace InteractionExample
{
    public class ExplosionShooterLogic
    {
        private readonly float ExplosionRadius;
        private readonly float ExplosionForce;

        public ExplosionShooterLogic(float explosionRadius, float explosionForce)
        {
            ExplosionRadius = explosionRadius;
            ExplosionForce = explosionForce;
        }

        public bool Shoot(Ray ray, out Vector3 explosionPoint)
        {
            RaycastHit[] hits = HitsCommon.GetHitsByRaySorted(ray);

            if (hits.Length == 0)
            {
                explosionPoint = Vector3.zero;
                return false;
            }

            explosionPoint = hits[0].point;

            Collider[] exploidItems = Physics.OverlapSphere(explosionPoint, ExplosionRadius);
            Debug.Log($"Explosion pos: {explosionPoint}, items: {exploidItems.Length}");

            foreach (Collider item in exploidItems)
            {
                if (item.TryGetComponent(out Rigidbody rb))
                    rb.AddExplosionForce(ExplosionForce, explosionPoint, ExplosionRadius);
            }

            return true;
        }
    }
}