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

        public Guid Id { get; }
    }
}
