using GhostWaiter.Control;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public abstract class UpdatableBehaviourBase
    {
        protected Transform Owner { get; private set; }

        protected UpdatableBehaviourBase(Transform owner)
        {
            Owner = owner;
        }

        public abstract void Update(float deltaTime);
    }
}