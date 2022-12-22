using System;
using System.Net.Http;
using Castle.Windsor;
using Newtonsoft.Json;
using RabbitMQCommon;
using RabbitMQCommon.Messages;
using RabbitMQCommon.PubSub;

namespace DockerRabbitMQRedisTestApp
{
    class Program
    {
        private static IWindsorContainer _container;
        
        static void Main(string[] args)
        {
            EnvironmentContainer.InitContainer();
            _container = EnvironmentContainer.Container;
            
            while (true)
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("\t1. Send message.");
                Console.WriteLine("\t2. Send random message.");
                Console.WriteLine("\t3. Exit.");
                var input = Console.ReadKey();
                
                byte.TryParse(input.KeyChar.ToString(), out var commandId);
        
                if (commandId == 1)
                {
                    SendMessage();
                }
                else if (commandId == 2)
                {
                    SendRandomMessage();
                }
                else if (commandId == 3)
                {
                    break;
                }
            }
            
            RabbitMQCommon.PubSub.RabbitMQ.Point.Dispose();
        }
        
        static void SendMessage()
        {
            Console.WriteLine("Enter your message:");
            var message = new DefaultMessage(Console.ReadLine());
        
            var publicMessageService = _container.Resolve<IMessagePublishService>();
            
            try
            {
                Redis.AddMessage(message);
                publicMessageService.PublishMessage(message);
                Console.WriteLine($"Message sent: {message}");
                Console.WriteLine();
            }
            finally
            {
                _container.Release(publicMessageService);
            }
        }
        
        static void SendRandomMessage()
        {
            var message = new RandomMessage(GetRandomMessage());
            var publicMessageService = _container.Resolve<IMessagePublishService>();
            
            try
            {
                Redis.AddMessage(message);
                publicMessageService.PublishMessage(message);
                Console.WriteLine($"Message sent: {message}");
                Console.WriteLine();
            }
            finally
            {
                _container.Release(publicMessageService);
            }
        }
        
        private static HttpClient _httpClient = new HttpClient();
        
        static string GetRandomMessage()
        {
            const string api = "https://fish-text.ru/get?type=sentence&number=5&format=json";
            
            var result = _httpClient.GetStringAsync(api).Result;
            var dataResult = JsonConvert.DeserializeObject<DataResult>(result);
            
            return dataResult.Text;
        }
        
        private class DataResult
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            
            [JsonProperty("text")]
            public string Text { get; set; }
        } 
    }
}