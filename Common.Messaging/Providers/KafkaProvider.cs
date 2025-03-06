using Common.Messaging.Abstract;
using Confluent.Kafka;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Messaging
{
    public class KafkaProvider : IMessageQueueProvider
    {
        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        public KafkaProvider(string host, int port, string groupId)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = $"{host}:{port}",
                BatchSize = 32 * 1024, // 32 KB
                LingerMs = 5 // 5 ms
            };

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = $"{host}:{port}",
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                FetchMinBytes = 1 * 1024, // 1 KB
                FetchWaitMaxMs = 100 // 100 ms
            };
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
            var messageValue = JsonSerializer.Serialize(message);
            await producer.ProduceAsync(topic, new Message<Null, string> { Value = messageValue });
            producer.Flush(TimeSpan.FromSeconds(5)); // Üretici buffer'ı boşaltıyor
        }

        public void Consume<T>(string topic, Action<T> onMessageReceived, CancellationToken cancellationToken)
        {
            using var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
            consumer.Subscribe(topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(cancellationToken);
                    if (consumeResult?.Message?.Value != null)
                    {
                        var message = JsonSerializer.Deserialize<T>(consumeResult.Message.Value);
                        onMessageReceived?.Invoke(message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close(); // Graceful shutdown
            }
        }
    }
}
