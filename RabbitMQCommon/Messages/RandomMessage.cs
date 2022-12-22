using System;

namespace RabbitMQCommon.Messages
{
    public class RandomMessage : BaseMessage
    {
        public RandomMessage() : base()
        {
        }
        
        public RandomMessage(string message) : base(message)
        {
        }
        
        public RandomMessage(Guid guid, string message) : base(guid, message)
        {
        }
    }
}