using System;

using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    [Serializable]
    public abstract class LoseConditionBase : ILoseCondition
    {
        private readonly ReactiveVariable<bool>          _isLostVar = new();
        public           IReactiveVariableReadonly<bool> IsLost => _isLostVar;

        public abstract void Update(float deltaTime);

        public virtual void Init(LoseInitData data) => _isLostVar.Value = false;
        protected      void TriggerLost()           => _isLostVar.Value = true;
    }
}