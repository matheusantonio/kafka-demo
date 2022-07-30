using MongoDB.Driver;
using Shared.Entities;
using Shared.Repositories;

namespace Publisher.Infrastructure.Persistence.Mongo
{
    public abstract class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        protected readonly IMongoCollection<T> _source;

        protected Repository(IMongoDatabase database, string collection)
        {
            _source = database.GetCollection<T>(collection);
        }

        public async Task<T> GetById(Guid id)
        {
            return await _source.Find(x => x.Id == id).FirstAsync();
        }

        public async Task Remove(Guid id)
        {
            await _source.DeleteOneAsync(x => x.Id == id);
        }

        public async Task Create(T aggregate)
        {
            await _source.InsertOneAsync(aggregate);
        }

        public async Task Update(T aggregate)
        {
            await _source.ReplaceOneAsync(x => x.Id == aggregate.Id, aggregate);
        }
    }
}
