namespace Navigation.Interfaces
{
    public interface IEventSubscriber
    {
        void Callback(string eventName, object data);
    }
}