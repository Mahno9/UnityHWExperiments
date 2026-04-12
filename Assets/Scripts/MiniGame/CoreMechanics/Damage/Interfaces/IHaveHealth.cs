using Navigation.Utils;

namespace MiniGame.CoreMechanics.Damage
{
    public interface IHaveHealth
    {
        public IReactiveVariableReadonly<float> Health { get; }
    }
}