using Shared.Events;

namespace Publisher.Domain.Events
{
    public class MessageUpvotedEvent : IDomainEvent
    {
        public Guid MessageId { get; set; }
    }
}
