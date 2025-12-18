using Navigation.Damage.Behaviours;
using Navigation.Interfaces;

namespace Navigation.Damage.Interfaces
{
    public interface IHealth : IDamageable
    {
        float RemainHealth { get; }

        bool  IsDead();

        void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber);
    }
}