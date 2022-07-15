using Publisher.Domain.Commands;
using Shared.Commands;
using Shared.Commands.Handlers;

namespace Publisher.Domain.Handlers.Commands
{
    public class MessageCommandHandler : ICommandHandler<CreateMessageCommand>,
                                         ICommandHandler<RemoveMessageCommand>
    {
        public ICommandResult Handle(CreateMessageCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(RemoveMessageCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
