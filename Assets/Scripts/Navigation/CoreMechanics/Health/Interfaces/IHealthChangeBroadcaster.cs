namespace Navigation.Damage.Interfaces
{
    public interface IHealthChangeBroadcaster
    {
        void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber);
    }
}