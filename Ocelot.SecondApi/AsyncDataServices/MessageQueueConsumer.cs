using Common.Messaging;
using Common.Messaging.Abstract;
using System.Text.Json;

namespace Ocelot.SecondApi.AsyncDataServices
{
    public class MessageQueueListener : MessageQueueConsumer
    {
        public MessageQueueListener(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void HandleMessage<T>(T message)
        {
            Console.WriteLine($"Mesaj alındı: {JsonSerializer.Serialize(message)}");
        }
    }
}
