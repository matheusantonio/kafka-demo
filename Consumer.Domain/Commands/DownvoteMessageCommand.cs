using Shared.Commands;

namespace Consumer.Domain.Commands
{
    public class DownvoteMessageCommand : ICommand
    {
        public Guid MessageId { get; set; }
    }
}
