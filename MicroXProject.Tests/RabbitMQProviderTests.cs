using Common.Messaging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CQRSWebApiProject.Tests
{
    public class RabbitMQProviderTests
    {
        private readonly Mock<IModel> _mockChannel;
        private readonly Mock<IConnection> _mockConnection;
        private readonly RabbitMQProvider _rabbitMQProvider;

        public RabbitMQProviderTests()
        {
            _mockChannel = new Mock<IModel>();
            _mockConnection = new Mock<IConnection>();
            var factory = new Mock<ConnectionFactory>();
            factory.Setup(f => f.CreateConnection()).Returns(_mockConnection.Object);
            _mockConnection.Setup(c => c.CreateModel()).Returns(_mockChannel.Object);

            _rabbitMQProvider = new RabbitMQProvider("localhost", 5672);
        }

        [Fact]
        public async Task PublishAsync_ShouldSendMessage()
        {
            // Arrange
            var queue = "test-queue";
            var message = "test-message";
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            // Act
            await _rabbitMQProvider.PublishAsync(queue, message);

            // Assert
            _mockChannel.Verify(c => c.BasicPublish("", queue, null, body), Times.Once);
        }

        [Fact]
        public void Consume_ShouldReceiveMessage()
        {
            // Arrange
            var queue = "test-queue";
            var cancellationToken = new CancellationTokenSource().Token;
            var messageReceived = false;
            var message = "test-message";
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            _mockChannel.Setup(c => c.BasicConsume(queue, true, It.IsAny<IBasicConsumer>()))
                .Callback<string, bool, IBasicConsumer>((q, autoAck, consumer) =>
                {
                    var eventArgs = new BasicDeliverEventArgs
                    {
                        Body = body
                    };
                    ((EventingBasicConsumer)consumer).HandleBasicDeliver("consumer_tag", 1, false, "exchange", "routing_key", null, new ReadOnlyMemory<byte>(body));
                });

            // Act
            _rabbitMQProvider.Consume<string>(queue, msg =>
            {
                messageReceived = true;
            }, cancellationToken);

            // Assert
            Assert.True(messageReceived);
        }
    }
}
