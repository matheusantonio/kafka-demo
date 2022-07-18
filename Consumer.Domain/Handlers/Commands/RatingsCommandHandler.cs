using Consumer.Domain.Commands;
using Consumer.Domain.Repositories;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.ExternalServices.Kafka;

namespace Consumer.Domain.Handlers.Commands
{
    public class RatingsCommandHandler : ICommandHandler<UpvoteMessageCommand>,
                                         ICommandHandler<DownvoteMessageCommand>
    {
        private readonly IMessageRepository _repository;
        private readonly IKafkaProducer _kafkaProducer;

        private static string UPVOTE_TOPIC = "messageUpvoted";
        private static string DOWNVOTE_TOPIC = "messageDownvoted";

        public RatingsCommandHandler(IMessageRepository repository, IKafkaProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public void Handle(UpvoteMessageCommand command)
        {
            var message = _repository.GetById(command.MessageId);

            message.Upvote();

            _repository.SaveChanges();

            _kafkaProducer.Produce(UPVOTE_TOPIC, command);
        }

        public void Handle(DownvoteMessageCommand command)
        {
            var message = _repository.GetById(command.MessageId);

            message.Downvote();

            _repository.SaveChanges();

            _kafkaProducer.Produce(DOWNVOTE_TOPIC, command);
        }
    }
}
