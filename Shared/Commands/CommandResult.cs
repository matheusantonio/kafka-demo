namespace Shared.Commands
{
    public interface ICommandResult
    {
        bool Valid { get; }
        string Message { get; }
    }
    public class CommandResult<T> : ICommandResult
    {
        public bool Valid { get; }
        public string Message { get; }
        public T? Content { get; }

        protected CommandResult(bool valid, T content) 
        {
            Valid = valid;
            Content = content;
            Message = "";
        }

        public CommandResult(bool valid, string message)
        {
            Valid = valid;
            Message = message;
            Content = default;
        }
    }
}
