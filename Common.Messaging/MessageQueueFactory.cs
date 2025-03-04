using Common.Messaging.Abstract;
using Common.Messaging.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Messaging
{
    public class MessageQueueFactory
    {
        private static IMessageQueueProvider _instance;
        private static readonly object _lock = new();

        
        public static IMessageQueueProvider CreateProvider()
        {
            return CreateProvider(GetConfiguration());
        }

        private static IMessageQueueProvider CreateProvider(RPCProvider providerType)
        {
            if (_instance == null)
            {
                lock (_lock)
                //lock, çoklu thread ortamlarında, aynı anda birden fazla iş parçacığının belirli bir kod bloğuna girmesini engellemek için kullanılır. Bu concurrency/eş zamanlılık sorunlarını çözmek amacıyla kullanılır,singleton gibi tek bir örneğin oluşturulması gerektiğinde tercih ediliyor.
                {
                    if (_instance == null)
                    {
                        _instance = providerType switch
                        {
                            RPCProvider.RabbitMQ => new RabbitMQProvider(GetHost(), GetPort()),
                            RPCProvider.Kafka => new KafkaProvider(),
                            RPCProvider.EgehanÇ => new EgehanProvider(),
                            _ => throw new Exception("Geçersiz RPC sağlayıcı!")
                        };
                    }
                }
            }
            return _instance;
        }

        private static RPCProvider GetConfiguration()
        {
            return RPCProvider.RabbitMQ;
        }
        private static string GetHost()
        {
            return "localhost";
        }
        private static int GetPort()
        {
            return 5672;
        }
    }

    public enum RPCProvider
    {
        RabbitMQ,
        Kafka,
        EgehanÇ
    }
}
