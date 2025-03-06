using AutoMapper;
using Common.Messaging;
using Common.Messaging.Abstract;
using CQRSWebApiProject.Business.Commands;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
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
        public CreateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository, IMapper mapper, IMessageQueueProvider messageQueueProvider)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.messageQueueProvider = messageQueueProvider;
        }


        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            //Request gelip mapperdan Entity.Concrete.Customer çevrilir
            var customer = mapper.Map<Entity.Concrete.Customer>(request.CreateCustomerRequest);
            var response = await repository.Add(customer);
            await repository.SaveChangesAsync();
            await messageQueueProvider.PublishAsync("test-topic", request);

            return mapper.Map<CreateCustomerResponse>(response);
        }
    }
}
