using Navigation.Utils;

namespace MiniGame.WinConditions
{
    public abstract class WinConditionBase : IWinCondition
    {
        private readonly ReactiveVariable<bool>          _isWinVar = new();
        public             IReactiveVariableReadonly<bool> IsWin => _isWinVar;

        protected void TriggerWin() => _isWinVar.Value = true;

        public abstract void Update(float     deltaTime);
        public abstract void Init(WinInitData data);
    }
}