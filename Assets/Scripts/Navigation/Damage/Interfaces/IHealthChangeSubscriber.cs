using Navigation.Interfaces;

namespace Navigation.Damage.Behaviours
{
    public interface IHealthChangeSubscriber
    {
        void DamageTaken(float damage);
    }
}