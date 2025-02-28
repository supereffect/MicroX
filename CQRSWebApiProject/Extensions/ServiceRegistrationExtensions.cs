using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
