﻿using Shared.Events;

namespace Consumer.Domain.Events
{
    public class MessageCreatedEvent : IDomainEvent
    {
        // Adicionar infos do Command de criar mensagem
        public Guid MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
