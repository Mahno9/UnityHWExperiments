using System;

using Navigation.Controllers;
using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    public interface ILoseCondition : IUpdatable
    {
        public IReactiveVariableReadonly<bool> IsLost();
    }
}