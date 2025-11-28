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
        private UpdatableBehaviourBase _aggroBehaviourBase;
        private UpdatableBehaviourBase _idleBehaviourBase;

        private BehaviourUpdater _behaviourUpdater;

        private SphereCollider _collider;

        private void Awake()
        {
            _behaviourUpdater = GetComponent<BehaviourUpdater>();
        }

        public void Initialize(UpdatableBehaviourBase idleBehaviourBase, UpdatableBehaviourBase aggroBehaviourBase)
        {
            CharacterMover mover = GetComponent<CharacterMover>();
            Assert.IsNotNull(mover);

            _idleBehaviourBase = idleBehaviourBase;
            _aggroBehaviourBase = aggroBehaviourBase;

            _behaviourUpdater.SetBehaviour(_idleBehaviourBase);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() is not null)
            {
                _behaviourUpdater.SetBehaviour(_aggroBehaviourBase);
                Debug.Log("Switch to aggro: " + _aggroBehaviourBase);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() is not null)
            {
                _behaviourUpdater.SetBehaviour(_idleBehaviourBase);
                Debug.Log("Switch to idle: " + _aggroBehaviourBase);
            }
        }
    }
}