using System.Collections.Generic;

namespace Navigation.Interfaces
{
    // Мне жалко это удалять, поэтому хочу оставить. Этот интрефейс и сабскрайбер пока не используются.
    public interface IEventBroadcaster
    {
        protected List<IEventSubscriber> Subscribers { get; set; }

        public void Subscribe(IEventSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Notify(string eventName, object data)
        {
            foreach (IEventSubscriber subscriber in Subscribers)
                subscriber.Callback(eventName, data);
        }
    }
}