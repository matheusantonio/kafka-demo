using Shared.Events;

namespace Publisher.Domain.Events
{
    public class MessageDownvotedEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OcurredAt { get; set; }
    }
}
