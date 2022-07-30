namespace Shared.Events
{
    public interface IEventRouter
    {
        Task Send<T>(T command) where T : IDomainEvent;
    }
}
