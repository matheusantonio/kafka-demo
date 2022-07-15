using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Infrastructure.ExternalServices.Kafka
{
    public class KafkaConfig
    {
        public Dictionary<string, string> Topics { get; }
        public string GroupId { get; }
        public string BootstrapServer { get; }
    }
}
