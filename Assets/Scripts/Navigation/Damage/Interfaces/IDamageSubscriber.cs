using Navigation.Interfaces;

namespace Navigation.Damage.Interfaces
{
    public interface IDamageSubscriber
    {
        void DamageTaken(IDamageable target, float damage);
    }
}