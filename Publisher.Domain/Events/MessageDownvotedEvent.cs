using Shared.Events;

namespace Publisher.Domain.Events
{
    public class MessageDownvotedEvent : IDomainEvent
    {
        public Guid MessageId { get; set; }
    }
}
