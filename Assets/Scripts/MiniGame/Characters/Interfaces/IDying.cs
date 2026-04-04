using Navigation.Utils;

namespace MiniGame.Characters
{
    public interface IDying
    {
        IReactiveVariableReadonly<bool> IsDead { get; }
    }
}