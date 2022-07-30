using Consumer.Domain.Entities;
using Consumer.Domain.Repositories;
using Consumer.Infrastructure.Persistence.Core.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Consumer.Infrastructure.Repositories
{
    public class MessageRepository : Repository<MessageDomain>, IMessageRepository
    {
        public MessageRepository(IMongoDatabase database, IOptions<MongoSettings> settings) : base(database, settings.Value.MessageCollectionName)
        {
        }

        public async Task<IEnumerable<MessageDomain>> List()
        {
            return await _source.Find(x => true).ToListAsync();
        }
    }
}
