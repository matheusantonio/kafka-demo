using Publisher.Domain.Events;
using Publisher.Domain.Repositories;
using Shared.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Domain.Handlers.Events
{
    public class RatingsEventHandler : IEventHandler<MessageUpvotedEvent>,
                                       IEventHandler<MessageDownvotedEvent>
    {
        private readonly IMessageRepository _repository;

        public RatingsEventHandler(IMessageRepository repository)
        {
            _repository = repository;
        }

        public void Handle(MessageUpvotedEvent command)
        {
            var message = _repository.GetById(command.MessageId);

            message.Upvote();

            _repository.SaveChanges();
        }

        public void Handle(MessageDownvotedEvent command)
        {
            var message = _repository.GetById(command.MessageId);

            message.Downvote();

            _repository.SaveChanges();
        }
    }
}
