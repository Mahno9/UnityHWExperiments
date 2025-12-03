using GhostWaiter.Control;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate.Behaviours
{
    public abstract class MovableBehaviourBase : IUpdatableBehaviour
    {
        private readonly CharacterMover _mover;
        protected Transform Owner { private set; get; }

        protected MovableBehaviourBase(Transform owner)
        {
            Owner = owner;

            _mover = Owner.GetComponent<CharacterMover>();
            Assert.IsNotNull(_mover);
        }

        protected void MoveTo(Vector3 directionNorm)
        {
            _mover.ProcessMoveTo(directionNorm);
        }

        public abstract void Update(float deltaTime);
    }
}