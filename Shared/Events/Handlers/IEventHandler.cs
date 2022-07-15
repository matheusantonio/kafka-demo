namespace Shared.Events.Handlers
{
    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T command);
    }
}
