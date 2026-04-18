using System;

using Navigation.Utils;

using UnityEngine;

namespace MiniGame.WinConditions
{
    [Serializable]
    public abstract class WinConditionBase : IWinCondition
    {
        private readonly ReactiveVariable<bool>          _isWinVar = new();
        public           IReactiveVariableReadonly<bool> IsWin => _isWinVar;

        public abstract void Update(float deltaTime);

        protected      void TriggerWin()           => _isWinVar.Value = true;
        public virtual void Init(WinInitData data) => _isWinVar.Value = false;
    }
}