using Navigation.Damage.Behaviours;

namespace Navigation.Interfaces
{
    public interface IDamageable
    {
        float RemainHealth { get; }
        void TakeDamage(float damage);

        bool IsDead();

        void SubscribeOnDamage(IDamageSubscriber subscriber);
    }
}