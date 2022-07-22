using Shared.Events;

namespace Consumer.Domain.Events
{
    public class MessageRemovedEvent : IDomainEvent
    {
        public Guid MessageId { get; set; }
    }
}
