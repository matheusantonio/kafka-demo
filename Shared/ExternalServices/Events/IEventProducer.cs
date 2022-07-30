namespace Shared.ExternalServices.Events
{
    public interface IEventProducer
    {
        Task Produce(string topic, object message);

        Task CreateTopicMaybe(string topic);
    }
}
