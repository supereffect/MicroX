using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.Validators;
using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;
using CQRSWebApiProject.DAL.Concrete.Redis.Concrete;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace Kanbersky.Customer.Business.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        //buradaki extensionları açıkalyaylım.
        //burada bizim köprümüz olan MediatR kütüphansini servisten ayağa kaldırıyoruz. controllerdan handlelara bize köprü oluyor...

        public static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        //Fluentvalidation kütüphanesi ile gelen istekleri kolay ve kod kalabalığı olmadan validation yapmamıza yarayan kütüphanemiz var
        //apimizi bu servise abone ediyoruz...

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddSingleton<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
        }
        public static void RedisConfiguration(this IServiceCollection services)
        {
            var redisConfiguration = ConfigurationOptions.Parse("localhost:7001,localhost:7002,localhost:7003,localhost:7004,localhost:7005,localhost:7006");
            redisConfiguration.ResolveDns = true; // DNS çözümleme aktif etmek önemli.
            redisConfiguration.ConnectTimeout = 10000;
            redisConfiguration.AbortOnConnectFail = false;
            ConnectionMultiplexer _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConfiguration);
            services.AddSingleton<IConnectionMultiplexer>(_connectionMultiplexer);
            services.AddSingleton<IRedisService, RedisService>();

        }
    }
}
