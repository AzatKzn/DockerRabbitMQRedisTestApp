using System;

namespace RabbitMQCommon.PubSub
{
    public interface IMessageSubscriberService
    {
        void Subscribe<TMessage>(Action<TMessage> handler);
    }
}