using System;

using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    [Serializable]
    public abstract class LoseConditionBase : ILoseCondition
    {
        public           IReactiveVariableReadonly<bool> IsLost => _isLostVar;

        private readonly ReactiveVariable<bool>          _isLostVar = new();

        public LoseConditionBase()
        {
            _isLostVar.Value = false;
        }

        public abstract void Update(float deltaTime);

        protected void TriggerLost()
        {
            _isLostVar.Value = true;
        }
    }
}