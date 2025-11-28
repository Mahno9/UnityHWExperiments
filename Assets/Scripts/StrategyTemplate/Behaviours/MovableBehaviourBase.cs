using GhostWaiter.Control;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate.Behaviours
{
    public abstract class MovableBehaviourBase : UpdatableBehaviourBase
    {
        private readonly CharacterMover _mover;

        protected MovableBehaviourBase(Transform owner) : base(owner)
        {
            _mover = Owner.GetComponent<CharacterMover>();
            Assert.IsNotNull(_mover);
        }

        protected void MoveTo(Vector3 directionNorm)
        {
            _mover.ProcessMoveTo(directionNorm);
        }
    }
}