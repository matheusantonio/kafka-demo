namespace Consumer.Infrastructure.Persistence.Core.Mongo
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MessageCollectionName { get; set; }
    }

    public record MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MessageCollectionName { get; set; }
    }

}
