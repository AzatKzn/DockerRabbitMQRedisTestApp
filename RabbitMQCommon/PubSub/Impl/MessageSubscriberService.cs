using System;

namespace RabbitMQCommon.PubSub.Impl
{
    public class MessageSubscriberService : IMessageSubscriberService
    {
        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            RabbitMQ.Point.SubscribeMessage(handler);
        }
    }
}