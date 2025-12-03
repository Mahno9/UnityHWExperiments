using StrategyTemplate.Behaviours;

using UnityEngine;
using UnityEngine.Assertions;

namespace StrategyTemplate
{
    public class BehaviourUpdater : MonoBehaviour
    {
        [SerializeField] private IUpdatableBehaviour _behaviour;

        private void Update()
        {
            Assert.IsNotNull(_behaviour);

            _behaviour.Update(Time.deltaTime);
        }

        public void SetBehaviour(IUpdatableBehaviour newBehaviour)
        {
            _behaviour = newBehaviour;
        }
    }
}