using System;

namespace RabbitMQCommon.Messages
{
    public class DefaultMessage : BaseMessage
    {
        public DefaultMessage() : base()
        {
        }
        
        public DefaultMessage(string message) : base(message)
        {
        }
        
        public DefaultMessage(Guid guid, string message) : base(guid, message)
        {
        }
    }
}