namespace Navigation.Common.Interfaces
{
    public interface IEventSubscriber
    {
        void Callback(string eventName, object data);
    }
}