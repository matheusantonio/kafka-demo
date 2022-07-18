namespace Shared.Commands.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        public void Handle(T command);
    }
}
