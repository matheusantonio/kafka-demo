using Shared.Commands;

namespace Publisher.Domain.Commands
{
    public class RemoveMessageCommand : ICommand
    {
        public Guid MessageId { get; set; }
    }
}
