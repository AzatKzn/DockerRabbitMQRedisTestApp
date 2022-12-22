namespace RabbitMQCommon.PubSub.Impl
{
    public class MessagePublishService : IMessagePublishService
    {
        public void PublishMessage<TMessage>(TMessage message)
        {
            RabbitMQ.Point.PublishMessage(message);
        }
    }
}