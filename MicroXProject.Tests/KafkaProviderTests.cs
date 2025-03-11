using Common.Messaging;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using Common.Messaging.Abstract;
using System.Timers;

namespace CQRSWebApiProject.Tests
{
    public class KafkaProviderTests
    {
        private readonly Mock<IMessageQueueProvider> _mockKafkaProvider;

        public KafkaProviderTests()
        {
            _mockKafkaProvider = new Mock<IMessageQueueProvider>();
        }

        [Fact]
        public async Task PublishAsync_ShouldSendMessage()
        {
            // Arrange
            var topic = "test-topic";
            var message = "test-message";

            // Act
            await _mockKafkaProvider.Object.PublishAsync(topic, message);

            // Assert
            _mockKafkaProvider.Verify(x => x.PublishAsync(topic, message), Times.Once);
        }

        [Fact]
        public void Consume_ShouldReceiveMessage()
        {
            // Arrange
            var topic = "test-topic";
            var cancellationToken = new CancellationTokenSource().Token;
            var messageReceived = false;

            _mockKafkaProvider.Setup(x => x.Consume<string>(topic, It.IsAny<Action<string>>(), cancellationToken))
                .Callback<string, Action<string>, CancellationToken>((t, callback, ct) =>
                {
                    callback("test-message");
                });

            // Act
            _mockKafkaProvider.Object.Consume<string>(topic, message =>
            {
                messageReceived = true;
            }, cancellationToken);

            // Assert
            Assert.True(messageReceived);
        }
    }
}