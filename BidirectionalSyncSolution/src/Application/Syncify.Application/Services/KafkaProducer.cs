using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Syncify.Application.Services
{
    public class KafkaProducer
    {
        private readonly string _connectionString;
        private readonly string _topicName;

        public KafkaProducer(string connectionString, string topicName)
        {
            _connectionString = connectionString;  // Full Event Hub connection string
            _topicName = topicName;
        }

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "", // ✅ Only hostname + :9093
                SecurityProtocol = SecurityProtocol.SaslSsl,                           // ✅ Required
                SaslMechanism = SaslMechanism.Plain,                                   // ✅ Required
                SaslUsername = "$ConnectionString",                                    // ✅ Literal string
                SaslPassword = "" // ✅ Full string with exact punctuation and no modifications
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message });
                    Console.WriteLine($"✅ Message sent to {result.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"❌ Kafka Produce Error: {e.Message}");
                }
            }
        }
    }
}
