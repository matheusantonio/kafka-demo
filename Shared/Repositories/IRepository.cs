using Shared.Entities;

namespace Shared.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> GetById(Guid id);
        Task Create(T aggregate);

        Task Update(T aggregate);
        Task Remove(Guid id);
    }
}
