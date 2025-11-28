using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public class HoldBehaviour : UpdatableBehaviourBase
    {
        public HoldBehaviour(Transform owner) : base(owner)
        {
        }

        public override void Update(float deltaTime)
        {
            // Do nothing (defend area ofc)
        }
    }
}