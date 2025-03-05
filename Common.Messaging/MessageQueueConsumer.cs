using Common.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace Ocelot.SecondApi.AsyncDataServices
{
    public abstract class MessageQueueConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        protected string channel;
        public MessageQueueConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var messageQueue = MessageQueueFactory.CreateProvider();

            messageQueue.Consume<object>(channel, message =>
            {
                Console.WriteLine($"Mesaj alındı: {JsonSerializer.Serialize(message)}");
                HandleMessage(message);
            }, stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken); // Sürekli dinlemeye devam et
        }
        protected abstract void HandleMessage<T>(T message);
    }
}
