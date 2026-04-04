using Navigation.Utils;

namespace MiniGame.WinConditions
{
    public abstract class WinConditionBase : IWinCondition
    {
        protected ReactiveVariable<bool> IsWinVar;

        public abstract void Update(float deltaTime);

        public IReactiveVariableReadonly<bool> IsWin() => IsWinVar;
    }
}