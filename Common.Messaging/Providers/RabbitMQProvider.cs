namespace Common.Messaging
{
    using System.Text;
    using System.Text.Json;
    using Common.Messaging.Abstract;
    using Microsoft.Extensions.Configuration;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;


    public class RabbitMQProvider : IMessageQueueProvider
    {
        private  IConnection _connection;
        private IModel _channel;

        public RabbitMQProvider(string hostname,int port)
        {

            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                //You should choose best approach to your project reqs. Dirext / Fanout / Topic / Header...
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;
                Console.WriteLine("--> Connected to MessageBus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't connect message bus: {ex.Message}");
            }
        }

        public async Task PublishAsync<T>(string queue, T message)
        {
            _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish("", queue, null, body);
        }

        public void Consume<T>(string queue, Action<T> onMessageReceived, CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
                if (message != null)
                {
                    onMessageReceived(message);
                }
            };

            _channel.BasicConsume(queue, true, consumer);
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Conn shutdown");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
        
    }
    
}