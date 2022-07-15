namespace Shared.Events
{
    public interface IEventRouter
    {
        void Send<T>(T command) where T : IDomainEvent;
    }
}
