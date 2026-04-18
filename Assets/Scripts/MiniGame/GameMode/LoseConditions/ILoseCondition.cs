using System;

using MiniGame.Characters;

using Navigation.Controllers;
using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    public interface ILoseCondition : IUpdatable
    {
        public IReactiveVariableReadonly<bool> IsLost { get; }

        public void Init(LoseInitData data);
    }
}