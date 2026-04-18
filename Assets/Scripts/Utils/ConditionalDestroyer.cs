using System;

using UnityEngine;

namespace Common.Utils
{
    public class ConditionalDestroyer : MonoBehaviour
    {
        private Func<bool> _shouldRemove;

        public void Initialize(Func<bool> shouldRemove)
        {
            _shouldRemove = shouldRemove;
        }

        public void Update()
        {
            if (_shouldRemove is not null && _shouldRemove.Invoke())
                Destroy(gameObject);
        }
    }
}