namespace Shared.ExternalServices.Kafka
{
    public interface IKafkaProducer
    {
        Task Produce(string topic, object message);

        Task CreateTopicMaybe(string topic);
    }
}
