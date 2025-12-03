using System;

using GhostWaiter.Control;

using StrategyTemplate.Behaviours;
using StrategyTemplate.Markers;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate
{
    [RequireComponent(typeof(BehaviourUpdater))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(CharacterMover))] // Required by behaviours
    public class BehaviourPicker : MonoBehaviour
    {
        private IUpdatableBehaviour _aggroBehaviour;
        private IUpdatableBehaviour _idleBehaviour;

        private BehaviourUpdater _behaviourUpdater;

        private SphereCollider _collider;
        private Transform _triggerTransform;

        private void Awake()
        {
            _behaviourUpdater = GetComponent<BehaviourUpdater>();
        }

        public void Initialize(IUpdatableBehaviour idleBehaviour, IUpdatableBehaviour aggroBehaviour, Transform triggerTransform)
        {
            CharacterMover mover = GetComponent<CharacterMover>();
            Assert.IsNotNull(mover);

            _idleBehaviour = idleBehaviour;
            _aggroBehaviour = aggroBehaviour;

            _behaviourUpdater.SetBehaviour(_idleBehaviour);

            _triggerTransform = triggerTransform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform != _triggerTransform)
                return;

            _behaviourUpdater.SetBehaviour(_aggroBehaviour);
            Debug.Log("Switch to aggro: " + _aggroBehaviour);
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.transform != _triggerTransform)
                return;

            _behaviourUpdater.SetBehaviour(_idleBehaviour);
            Debug.Log("Switch to idle: " + _aggroBehaviour);
        }
    }
}