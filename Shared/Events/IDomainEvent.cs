using Shared.Commands;

namespace Shared.Events
{
    public interface IDomainEvent
    {
        public Guid Id { get; }
        public DateTime OcurredAt { get; }
    }
}
