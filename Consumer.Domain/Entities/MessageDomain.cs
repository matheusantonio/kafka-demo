using Shared.Entities;

namespace Consumer.Domain.Entities
{
    public class MessageDomain : Entity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string Author { get; private set; }
        public int Upvotes { get; private set; }
        public int Downvotes { get; private set; }
        public int Rating => Upvotes - Downvotes;
        public DateTime CreatedAt { get; set; }

        public MessageDomain(string title, string content, string author, DateTime createdAt)
        {
            Title = title;
            Content = content;
            Author = author;
            Upvotes = 0;
            Downvotes = 0;
            CreatedAt = createdAt;
        }

        public void Upvote()
        {
            Upvotes++;
        }

        public void Downvote()
        {
            Downvotes++;
        }
    }
}
