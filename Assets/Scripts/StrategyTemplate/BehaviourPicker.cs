using System;

using StrategyTemplate.Behaviours;
using StrategyTemplate.Markers;

using UnityEngine;

namespace StrategyTemplate
{
    [RequireComponent(typeof(BehaviourUpdater))]
    [RequireComponent(typeof(SphereCollider))]
    public class BehaviourPicker : MonoBehaviour
    {
        private IUpdatableBehaviour _aggroBehaviour;
        private IUpdatableBehaviour _idleBehaviour;

        private BehaviourUpdater _behaviourUpdater;

        private SphereCollider _collider;

        private void Awake()
        {
            _behaviourUpdater = GetComponent<BehaviourUpdater>();
        }

        public void Initialize(IUpdatableBehaviour idleBehaviour, IUpdatableBehaviour aggroBehaviour)
        {
            _idleBehaviour = idleBehaviour;
            _aggroBehaviour = aggroBehaviour;

            _behaviourUpdater.SetBehaviour(_idleBehaviour);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() is not null)
            {
                _behaviourUpdater.SetBehaviour(_aggroBehaviour);
                Debug.Log("Switch to aggro: " + _aggroBehaviour);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<Player>() is not null)
            {
                _behaviourUpdater.SetBehaviour(_idleBehaviour);
                Debug.Log("Switch to idle: " + _aggroBehaviour);
            }
        }
    }
}