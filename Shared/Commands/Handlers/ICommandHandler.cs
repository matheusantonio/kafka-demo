namespace Shared.Commands.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        public ICommandResult Handle(T command);
    }
}
