using Consumer.Domain.Commands;
using Consumer.Domain.Repositories;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.ExternalServices.Events;

namespace Consumer.Domain.Handlers.Commands
{
    public class RatingsCommandHandler : ICommandHandler<UpvoteMessageCommand>,
                                         ICommandHandler<DownvoteMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly IEventProducer _kafkaProducer;

        private static string UPVOTE_TOPIC = "messageUpvoted";
        private static string DOWNVOTE_TOPIC = "messageDownvoted";

        public RatingsCommandHandler(IMessageRepository repository, IEventProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task Handle(UpvoteMessageCommand command)
        {
            var message = await _repository.GetById(command.MessageId);

            message.Upvote();

            await _repository.Update(message);

            await _kafkaProducer.Produce(UPVOTE_TOPIC, command);
        }

        public async Task Handle(DownvoteMessageCommand command)
        {
            var message = await _repository.GetById(command.MessageId);

            message.Downvote();

            await _repository.Update(message);

            await _kafkaProducer.Produce(DOWNVOTE_TOPIC, command);
        }
    }
}
