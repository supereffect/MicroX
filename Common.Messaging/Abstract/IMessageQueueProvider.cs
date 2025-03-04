using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messaging.Abstract
{
    public interface IMessageQueueProvider
    {
        Task PublishAsync<T>(string topic, T message);
        public void Consume<T>(string queue, Action<T> onMessageReceived, CancellationToken cancellationToken);

    }
}
