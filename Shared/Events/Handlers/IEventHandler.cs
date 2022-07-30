namespace Shared.Events.Handlers
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        Task Handle(T command);
    }
}
