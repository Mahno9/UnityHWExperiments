using Navigation.Utils;

namespace MiniGame.WinConditions
{
    public abstract class WinConditionBase : IWinCondition
    {
        private readonly ReactiveVariable<bool> _isWinVar = new();

        public WinConditionBase()
        {
            _isWinVar.Value = false;
        }

        public IReactiveVariableReadonly<bool> IsWin => _isWinVar;

        public abstract void Update(float deltaTime);

        protected void TriggerWin()
        {
            _isWinVar.Value = true;
        }
    }
}