namespace Navigation.Movement.Interfaces
{
    public interface IMovePointBroadcaster
    {
        void SubscribeOnMovePoints(IMovePointSubscriber subscriber);
    }
}