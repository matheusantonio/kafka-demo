using Shared.Commands;

namespace Consumer.Domain.Commands
{
    public class UpvoteMessageCommand : ICommand
    {
        public Guid MessageId { get; set; }
    }
}
