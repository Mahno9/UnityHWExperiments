using UnityEngine;

namespace InteractionExample.PointerInteractions.Explosion.Interfaces
{
    public interface IForceable
    {
        void AddForce(Vector3          direction,      float force);
        void AddExplosionForce(Vector3 explosionPoint, float explosionForce, float explosionRadius);
    }
}