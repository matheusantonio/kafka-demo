namespace Shared.Commands.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }
}
