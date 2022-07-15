namespace Shared.Commands
{
    public interface ICommandRouter
    {
        void Send<T>(T command) where T : ICommand;
    }
}
