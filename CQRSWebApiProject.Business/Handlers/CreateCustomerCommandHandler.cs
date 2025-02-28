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
        private readonly IActionResultTypeMapper mapper;

        public CreateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> , IActionResultTypeMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    }
}
