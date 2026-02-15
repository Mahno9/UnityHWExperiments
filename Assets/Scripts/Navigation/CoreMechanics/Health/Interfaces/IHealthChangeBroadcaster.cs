namespace Navigation.CoreMechanics.Health.Interfaces
{
    public interface IHealthChangeBroadcaster
    {
        void SubscribeOnHealthChange(IHealthChangeSubscriber subscriber);
    }
}