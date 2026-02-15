using Navigation.Characters.Interfaces;

namespace Navigation.CoreMechanics.Damage.Interfaces
{
    public interface ITargetsDetector
    {
        IDamageable[] GetTargets();
    }
}