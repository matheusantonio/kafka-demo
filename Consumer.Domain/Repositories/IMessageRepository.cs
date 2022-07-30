using Consumer.Domain.Entities;

namespace Consumer.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageDomain> GetById(Guid Id);

        Task<IEnumerable<MessageDomain>> List();

        Task Create(MessageDomain message);

        Task Remove(Guid id);

        Task Update(MessageDomain message);
    }
}
