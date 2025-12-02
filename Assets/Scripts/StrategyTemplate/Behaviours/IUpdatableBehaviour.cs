using GhostWaiter.Control;

using UnityEngine;

namespace StrategyTemplate.Behaviours
{
    public interface IUpdatableBehaviour
    {
        void Update(float deltaTime);
    }
}