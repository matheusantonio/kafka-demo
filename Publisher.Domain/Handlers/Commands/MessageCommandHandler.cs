using Publisher.Domain.Commands;
using Publisher.Domain.Entities;
using Publisher.Domain.Events;
using Publisher.Domain.Repositories;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.ExternalServices.Events;

namespace Publisher.Domain.Handlers.Commands
{
    public class MessageCommandHandler : ICommandHandler<CreateMessageCommand>,
                                         ICommandHandler<RemoveMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly IEventProducer _kafkaProducer;

        private static string MESSAGE_CREATED_TOPIC = "messageCreated";
        private static string MESSAGE_REMOVED_TOPIC = "messageRemoved";

        public MessageCommandHandler(IMessageRepository repository, IEventProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task Handle(CreateMessageCommand command)
        {
            var message = new MessageDomain(command.Title, command.Content, command.Author);

            await _repository.Create(message);

            await _kafkaProducer.Produce(MESSAGE_CREATED_TOPIC, new MessageCreatedEvent
            {
                MessageId = message.Id,
                Title = message.Title,
                Content = message.Content,
                Author = message.Author,
                CreatedAt = message.CreatedAt,
            });
        }

        public async Task Handle(RemoveMessageCommand command)
        {
            await _repository.Remove(command.MessageId);

            await _kafkaProducer.Produce(MESSAGE_REMOVED_TOPIC, command);
        }
    }
}
