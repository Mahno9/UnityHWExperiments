using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class HoldBehaviour : IUpdatableBehaviour
    {
        public HoldBehaviour(Transform owner)
        {
        }

        public void Update(float deltaTime)
        {
            // Do nothing (defend area ofc)
        }
    }
}