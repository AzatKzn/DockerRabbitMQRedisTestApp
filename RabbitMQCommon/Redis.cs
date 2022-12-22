using System;
using Newtonsoft.Json;
using RabbitMQCommon.Messages;
using StackExchange.Redis;

namespace RabbitMQCommon
{
    public static class Redis
    {
        private static IDatabase _database;
        
        static Redis()
        {
            InitConnection();
        }
        
        public static void AddMessage<TMessage>(TMessage message)
            where TMessage : BaseMessage
        {
            var key = $"{message.GetType().Name}:{message.Guid}";
            _database.StringSet(key, message.Message);
        }

        public static TMessage GetValue<TMessage>(Guid messageGuid)
            where TMessage : BaseMessage, new()
        {
            var key = $"{typeof(TMessage).Name}:{messageGuid}";
            var message = _database.StringGet(key).ToString();
            return new TMessage {Guid = messageGuid, Message = message};
        }

        private static void InitConnection()
        {
            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localhost", 6379);

            var redis = ConnectionMultiplexer.Connect(configuration);
            _database = redis.GetDatabase(1);
        }
    }
}