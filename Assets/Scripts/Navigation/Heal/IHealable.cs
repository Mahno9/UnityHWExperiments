using UnityEngine;

namespace Navigation.Heal
{
    public interface IHealable
    {
        void Heal(float healthPoints);
    }
}