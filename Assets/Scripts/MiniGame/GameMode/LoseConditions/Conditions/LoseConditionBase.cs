using System;

using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    [Serializable]
    public abstract class LoseConditionBase : ILoseCondition
    {
        private readonly ReactiveVariable<bool>          _isLostVar = new();
        public           IReactiveVariableReadonly<bool> IsLost => _isLostVar;

        public LoseConditionBase()
        {
            _isLostVar.Value = false;
        }

        public abstract void Update(float deltaTime);

        protected      void TriggerLost()           => _isLostVar.Value = true;
    }
}