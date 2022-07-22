namespace Publisher.Infrastructure.ExternalServices.Kafka
{
    public class KafkaSettings
    {
        public static string Kafka => "Kafka";
        public Dictionary<string, string> Topics { get; set; }
        public string GroupId { get; set; }
        public string BootstrapServer { get; set; }
    }
}
