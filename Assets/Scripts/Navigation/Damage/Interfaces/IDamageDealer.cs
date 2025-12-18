using Navigation.Damage.Behaviours;
using Navigation.Interfaces;

namespace Navigation.Damage.Interfaces
{
    public interface IDamageDealer
    {
        void SubscribeOnDamage(IDamageSubscriber subscriber);
        void DealDamage();
    }
}