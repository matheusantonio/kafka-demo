using Shared.Events;

namespace Consumer.Domain.Events
{
    public class MessageCreatedEvent : IDomainEvent
    {
        // Adicionar infos do Command de criar mensagem
        public Guid MessageId { get; set; }
        public Guid Id { get; set; }
        public DateTime OcurredAt { get; set; }
    }
}
