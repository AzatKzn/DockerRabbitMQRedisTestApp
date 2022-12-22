using System;
using System.Runtime.ConstrainedExecution;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQCommon.PubSub
{
    public class RabbitMQ : CriticalFinalizerObject, IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;

        private static RabbitMQ _point;
        private static object _syncObject = new();

        private bool _isDisposed = false;

        public static RabbitMQ Point
        {
            get
            {
                if (_point == null)
                {
                    lock (_syncObject)
                    {
                        if (_point == null)
                        {
                            _point = new RabbitMQ();
                        }
                    }
                }

                return _point;
            }
        }

        public RabbitMQ()
        {
            InitConnection();
        }

        private void InitConnection()
        {
            var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "localhost",
                Port = 5672
            };
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "test3",
                durable: true,
                exclusive: false,
                autoDelete: false);
        }
        
        public void PublishMessage<TMessage>(TMessage message)
        {
            var str = JsonConvert.SerializeObject(message);
            var body = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(str));
            _channel.BasicPublish(exchange: "", routingKey: "test3", body: body);
        }
        
        public void SubscribeMessage<TMessage>(Action<TMessage> handler)
        {
            InitConsumer();
            _consumer.Received += (sender, args) =>
            {
                var strMessage = Encoding.UTF8.GetString(args.Body.ToArray());

                var @object = JsonConvert.DeserializeObject(strMessage);
                if (@object is TMessage message)
                {
                    handler(message);
                }
            };
        }

        private void InitConsumer()
        {
            if (_consumer == null)
            {
                _consumer = new EventingBasicConsumer(_channel);
                _channel.BasicConsume(queue: "test3",
                    autoAck: true,
                    consumer: _consumer);
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RabbitMQ()
        {
            Dispose(false);    
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            
            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (_channel != null)
            {
                _channel.Dispose();
            }

            _isDisposed = true;
        }
    }
}