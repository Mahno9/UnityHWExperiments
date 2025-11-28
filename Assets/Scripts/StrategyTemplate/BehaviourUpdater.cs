using StrategyTemplate.Behaviours;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate
{
    public class BehaviourUpdater : MonoBehaviour
    {
        [SerializeField] private UpdatableBehaviourBase _behaviourBase;

        private void Update()
        {
            Assert.IsNotNull(_behaviourBase);

            _behaviourBase.Update(Time.deltaTime);
        }

        public void SetBehaviour(UpdatableBehaviourBase newBehaviourBase)
        {
            _behaviourBase = newBehaviourBase;
        }
    }
}