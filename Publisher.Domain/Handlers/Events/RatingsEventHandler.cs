using Publisher.Domain.Events;
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
        public void Handle(MessageUpvotedEvent command)
        {
            throw new NotImplementedException();
        }

        public void Handle(MessageDownvotedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}
