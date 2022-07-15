using Consumer.Domain.Events;
using Shared.Events.Handlers;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Domain.Handlers.Events
{
    public class MessageEventHandler : IEventHandler<MessageCreatedEvent>,
                                       IEventHandler<MessageRemovedEvent>
    {
        public void Handle(MessageRemovedEvent command)
        {
            throw new NotImplementedException();
        }

        public void Handle(MessageCreatedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}
