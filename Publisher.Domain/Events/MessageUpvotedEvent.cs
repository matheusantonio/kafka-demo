using Shared.Events;

namespace Publisher.Domain.Events
{
    public class MessageUpvotedEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OcurredAt { get; set; }
    }
}
