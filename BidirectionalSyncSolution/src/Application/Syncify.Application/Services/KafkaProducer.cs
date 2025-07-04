using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;  

namespace Syncify.Application.Services
{
    public class KafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;

        public KafkaProducer(string bootstrapServers, string topicName)
        {
            _bootstrapServers = bootstrapServers;
            _topicName = topicName;
        }

        public async Task SendMessageAsync(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = message });
                    Console.WriteLine($"Message sent to {result.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Error producing message: {e.Message}");
                }
            }
        }
    }
}
