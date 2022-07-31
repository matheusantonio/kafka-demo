using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
    }
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set;  }
    }
}
