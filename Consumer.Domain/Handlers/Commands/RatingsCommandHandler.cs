using Consumer.Domain.Commands;
using Shared.Commands;
using Shared.Commands.Handlers;

namespace Consumer.Domain.Handlers.Commands
{
    public class RatingsCommandHandler : ICommandHandler<UpvoteMessageCommand>,
                                         ICommandHandler<DownvoteMessageCommand>
    {
        public ICommandResult Handle(UpvoteMessageCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(DownvoteMessageCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
