namespace RabbitMQCommon.PubSub
{
    public interface IMessagePublishService
    {
        void PublishMessage<TMessage>(TMessage message);
    }
}