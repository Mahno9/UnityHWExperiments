using System;

using UnityEngine;

namespace Navigation.Behaviours
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