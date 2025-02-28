using AutoMapper;
using CQRSWebApiProject.Business.Commands;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        public CreateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //Request gelip mapperdan Entity.Concrete.Customer çevrilir
            var customer = mapper.Map<Entity.Concrete.Customer>(request.CreateCustomerRequest);
            var response = await repository.Add(customer);
            await repository.SaveChangesAsync();
            return  mapper.Map<CreateCustomerResponse>(response);   
        }
    }
}
