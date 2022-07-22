using Consumer.Domain.Entities;
using Consumer.Domain.Events;
using Consumer.Domain.Repositories;
using Shared.Events.Handlers;

namespace Consumer.Domain.Handlers.Events
{
    public class MessageEventHandler : IEventHandler<MessageCreatedEvent>,
                                       IEventHandler<MessageRemovedEvent>
    {
        private readonly IMessageRepository _repository;

        public MessageEventHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public void Handle(MessageRemovedEvent command)
        {
            _repository.Remove(command.MessageId);

            _repository.SaveChanges();
        }

        public void Handle(MessageCreatedEvent command)
        {
            var message = new MessageDomain(command.Title, command.Content, command.Author, command.CreatedAt);

            _repository.Create(message);
            _repository.SaveChanges();
        }
    }
}
