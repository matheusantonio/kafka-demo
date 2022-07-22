using Publisher.Domain.Entities;

namespace Publisher.Domain.Repositories
{
    public interface IMessageRepository
    {
        MessageDomain GetById(Guid Id);

        IEnumerable<MessageDomain> List();

        void Create(MessageDomain message);

        void Remove(Guid id);
        void SaveChanges();
    }
}
