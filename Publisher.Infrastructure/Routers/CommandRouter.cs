using Shared.Commands;
using Shared.Commands.Handlers;

namespace Publisher.Infrastructure.Routers
{
    public class CommandRouter : ICommandRouter
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandRouter(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }

        public async Task Send<T>(T command) where T : ICommand
        {
            var instance = _serviceProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;

            if (instance == null)
            {
                throw new InvalidOperationException("No Command Handler fount to handle this command.");
            }

            await instance.Handle(command);
        }
    }
}
