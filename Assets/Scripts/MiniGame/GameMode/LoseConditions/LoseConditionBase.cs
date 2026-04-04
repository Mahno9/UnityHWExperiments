using Navigation.Utils;

namespace MiniGame.LoseConditions
{
    public abstract class LoseConditionBase : ILoseCondition
    {
        public    IReactiveVariableReadonly<bool> IsLost()  => IsLostVar;
        protected ReactiveVariable<bool>          IsLostVar { get; }

        public abstract void Update(float deltaTime);
    }
}