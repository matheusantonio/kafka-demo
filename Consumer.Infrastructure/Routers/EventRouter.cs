using Shared.Events;
using Shared.Events.Handlers;

namespace Consumer.Infrastructure.Routers
{
    public class EventRouter : IEventRouter
    {
        private readonly IServiceProvider _serviceProvider;

        public EventRouter(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public async Task Send<T>(T command) where T : IDomainEvent
        {
            var instance = _serviceProvider.GetService(typeof(IEventHandler<T>)) as IEventHandler<T>;

            if (instance == null)
            {
                throw new InvalidOperationException("No Event Handler fount to handle this event.");
            }

            await instance.Handle(command);
        }
    }
}
