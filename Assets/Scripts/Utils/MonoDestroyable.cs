using System;

using UnityEngine;

namespace Common.Utils
{
    public class MonoDestroyable : MonoBehaviour
    {
        public event Action<MonoDestroyable> Destroyed;

        public bool IsDestroyed { get; private set; }

        public void Destroy()
        {
            Destroy(gameObject);

            IsDestroyed = true;
            Destroyed?.Invoke(this);
        }
    }
}