using UnityEngine;

namespace Navigation.Common.Behaviours
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