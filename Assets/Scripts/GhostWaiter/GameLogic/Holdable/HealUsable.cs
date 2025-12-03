using UnityEngine;

namespace GhostWaiter.GameLogic.Holdable
{
    public class HealUsable : Usable
    {
        [SerializeField] private int _healthIncreaseAmount = 20;

        public override void Use(GameObject targetObject)
        {
			if (targetObject.TryGetComponent(out Health health))
				health.AddHealth(_healthIncreaseAmount);

            base.Use(targetObject);
        }
    }
}
