using Shared.Events;

namespace Consumer.Domain.Events
{
    public class MessageRemovedEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OcurredAt { get; set; }
    }
}
