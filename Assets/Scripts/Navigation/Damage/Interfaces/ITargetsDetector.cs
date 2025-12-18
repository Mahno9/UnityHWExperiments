using Navigation.Interfaces;

namespace Navigation.Damage.Interfaces
{
    public interface ITargetsDetector
    {
        IDamageable[] GetTargets();
    }
}