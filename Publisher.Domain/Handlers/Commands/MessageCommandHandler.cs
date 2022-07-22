using Publisher.Domain.Commands;
using Publisher.Domain.Entities;
using Publisher.Domain.Events;
using Publisher.Domain.Repositories;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.ExternalServices.Kafka;

namespace Publisher.Domain.Handlers.Commands
{
    public class MessageCommandHandler : ICommandHandler<CreateMessageCommand>,
                                         ICommandHandler<RemoveMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly IKafkaProducer _kafkaProducer;

        private static string MESSAGE_CREATED_TOPIC = "messageCreated";
        private static string MESSAGE_REMOVED_TOPIC = "messageRemoved";

        public MessageCommandHandler(IMessageRepository repository, IKafkaProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public void Handle(CreateMessageCommand command)
        {
            var message = new MessageDomain(command.Title, command.Content, command.Author);

            _repository.Create(message);

            _kafkaProducer.Produce(MESSAGE_CREATED_TOPIC, new MessageCreatedEvent
            {
                Title = message.Title,
                Content = message.Content,
                Author = message.Author,
                CreatedAt = message.CreatedAt,
            });
        }

        public void Handle(RemoveMessageCommand command)
        {
            _repository.Remove(command.MessageId);

            _kafkaProducer.Produce(MESSAGE_REMOVED_TOPIC, command.MessageId);
        }
    }
}
