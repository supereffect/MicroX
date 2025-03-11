using AutoMapper;
using Common.Messaging;
using Common.Messaging.Abstract;
using CQRSWebApiProject.Business.Commands;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IGenericRepository<Entity.Concrete.Customer> repository;
        private readonly IMapper mapper;
        private readonly IMessageQueueProvider messageQueueProvider;
        private readonly IRedisService redisService;
        //private readonly ICacheService cacheService;
        public CreateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository, IMapper mapper, IMessageQueueProvider messageQueueProvider, IRedisService redisService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.messageQueueProvider = messageQueueProvider;
            this.redisService = redisService;
            //this.cacheService = cacheService;
        }


        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Redis'e veri yazılıyorF
            await redisService.SetValueAsync("test", "test");

           
            // Müşteri oluşturulup veritabanına ekleniyor
            var customer = mapper.Map<Entity.Concrete.Customer>(request.CreateCustomerRequest);
            var response = await repository.Add(customer);
            await repository.SaveChangesAsync();

            // Mesaj kuyruğuna veri gönderiliyor
            await messageQueueProvider.PublishAsync("test-topic", request);

            // Müşteri oluşturma işleminden dönen cevabı map'liyoruz
            return mapper.Map<CreateCustomerResponse>(response);
        }
    }
}
