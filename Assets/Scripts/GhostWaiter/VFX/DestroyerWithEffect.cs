using UnityEngine;
using UnityEngine.Assertions;

namespace GhostWaiter.VFX
{
    public class DestroyerWithEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _cleanExplosionFx;
        [SerializeField] private GameObject _dirtyExplosionFx;

        public void DestroyWithEffect(bool isClean)
        {
            Assert.IsNotNull(_cleanExplosionFx);
            Assert.IsNotNull(_dirtyExplosionFx);

            GameObject explosionFx = isClean ? _cleanExplosionFx : _dirtyExplosionFx;

            Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
            Destroy(gameObject);
        }
    }
}