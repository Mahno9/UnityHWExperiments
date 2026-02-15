using UnityEngine;

namespace Navigation.Utils
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Awake()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}