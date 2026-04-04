using Navigation.Controllers;
using Navigation.Utils;

namespace MiniGame.WinConditions
{
    public interface IWinCondition : IUpdatable
    {
        public IReactiveVariableReadonly<bool> IsWin();
    }
}