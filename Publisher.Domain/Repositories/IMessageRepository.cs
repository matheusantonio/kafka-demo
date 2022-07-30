using Publisher.Domain.Entities;

namespace Publisher.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageDomain> GetById(Guid Id);

        Task<IEnumerable<MessageDomain>> List();

        Task Create(MessageDomain message);

        Task Update(MessageDomain message);

        Task Remove(Guid id);
    }
}
