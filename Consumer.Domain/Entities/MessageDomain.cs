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

        public MessageDomain(string title, string content, string author, int upvotes, int downvotes, DateTime createdAt)
        {
            Title = title;
            Content = content;
            Author = author;
            Upvotes = upvotes;
            Downvotes = downvotes;
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
