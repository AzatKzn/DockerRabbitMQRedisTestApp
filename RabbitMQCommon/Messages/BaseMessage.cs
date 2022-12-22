using System;

namespace RabbitMQCommon.Messages
{
    public abstract class BaseMessage
    {
        public BaseMessage()
        {
            Guid = Guid.NewGuid();
        }
        
        public BaseMessage(string message) : this()
        {
            Message = message;
        }
        
        public BaseMessage(Guid guid, string message)
        {
            Guid = guid;
            Message = message;
        }
        
        public Guid Guid { get; set; }
        
        public string Message { get; set; }
    }
}