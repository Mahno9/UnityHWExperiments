using InteractionExample.PointerInteractions.Explosion.Interfaces;

using UnityEngine;

namespace InteractionExample.PointerInteractions.Explosion
{
    [RequireComponent(typeof(Rigidbody))]
    public class ForceableWithRigidbody : MonoBehaviour, IForceable
    {
        public void AddForce(Vector3 direction, float force)
        {
            GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Force);
        }

        public void AddExplosionForce(Vector3 explosionPoint, float explosionForce, float explosionRadius)
        {
            GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint, explosionRadius);
        }
    }
}