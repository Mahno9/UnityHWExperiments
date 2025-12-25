namespace Navigation.CoreMechanics.Movement.Interfaces
{
    public interface IMovePointBroadcaster
    {
        void SubscribeOnMovePoints(IMovePointSubscriber subscriber);
    }
}