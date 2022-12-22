using System;
using RabbitMQCommon;
using RabbitMQCommon.Messages;
using RabbitMQCommon.PubSub;

namespace RabbitMQConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            EnvironmentContainer.InitContainer();
            var messageSubscriberService = EnvironmentContainer.Resolve<IMessageSubscriberService>();

            try
            {
                messageSubscriberService.Subscribe<string>(HandleMessage);
                messageSubscriberService.Subscribe<DefaultMessage>(HandleMessage);
                messageSubscriberService.Subscribe<RandomMessage>(HandleMessage);
            }
            finally
            {
                EnvironmentContainer.Release(messageSubscriberService);
            }
        
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            
            RabbitMQCommon.PubSub.RabbitMQ.Point.Dispose();
        }
        
        static void HandleMessage(string message)
        {
            Console.WriteLine($"Message received: {message}");
            Console.WriteLine();
        }

        static void HandleMessage<TMessage>(TMessage message)
            where TMessage : BaseMessage, new()
        {
            string pref;
            
            if (message is DefaultMessage)
            {
                pref = "Default message";
            }
            else if (message is RandomMessage)
            {
                pref = "Random message";
            }
            else
            {
                pref = "Message";
            }

            var redisMessage = Redis.GetValue<TMessage>(message.Guid);
            
            Console.WriteLine($"{pref} received: {redisMessage.Message}");
            Console.WriteLine();
        }
    }
}