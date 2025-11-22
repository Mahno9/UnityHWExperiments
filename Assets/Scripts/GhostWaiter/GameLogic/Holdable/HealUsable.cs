using UnityEngine;
using UnityEngine.Assertions;

public class HealUsable : Usable
{
    [SerializeField] private int _healthIncreaseAmount = 20;

    public override void Use(GameObject targetObject)
    {
        if (Holder.TryGetComponent(out Health health))
            health.AddHealth(_healthIncreaseAmount);

        base.Use(targetObject);
    }
}
