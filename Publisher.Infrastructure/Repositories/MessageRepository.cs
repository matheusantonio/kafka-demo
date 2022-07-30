using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Publisher.Domain.Entities;
using Publisher.Domain.Repositories;
using Publisher.Infrastructure.Persistence.Mongo;

namespace Publisher.Infrastructure.Repositories
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
