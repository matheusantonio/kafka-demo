using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Infrastructure.ExternalServices.Kafka
{
    public class KafkaSettings
    {
        public static string Kafka => "Kafka";
        public Dictionary<string, string> Topics { get; set; }
        public string GroupId { get; set;  }
        public string BootstrapServer { get; set;  }
    }
}
