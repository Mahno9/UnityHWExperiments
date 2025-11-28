using StrategyTemplate.Actions;
using StrategyTemplate.Markers;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate.Behaviours
{
    public class FearDeathBehaviour : UpdatableBehaviourBase
    {
        private readonly IKillable _killable;

        public FearDeathBehaviour(Transform owner) : base(owner)
        {
            _killable = owner.GetComponent<IKillable>();
            Assert.IsNotNull(_killable);
        }

        public override void Update(float deltaTime)
        {
            _killable.Kill();
        }
    }
}