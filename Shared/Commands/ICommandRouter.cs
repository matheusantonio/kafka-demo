namespace Shared.Commands
{
    public interface ICommandRouter
    {
        Task Send<T>(T command) where T : ICommand;
    }
}
