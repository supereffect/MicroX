using Common.Messaging.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Messaging.Providers
{
    public class EgehanProvider : IMessageQueueProvider
    {
        public EgehanProvider()
        {

        }

        public void Consume<T>(string queue, Action<T> onMessageReceived, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task PublishAsync<T>(string topic, T message)
        {
            throw new NotImplementedException();
        }
    }
}
