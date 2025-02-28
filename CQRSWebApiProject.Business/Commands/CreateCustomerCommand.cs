using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Commands
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>    
    {
        public CreateCustomerRequest CreateCustomerRequest{ get; set; }  
        public CreateCustomerCommand(CreateCustomerRequest createCustomerRequest)

        {
            CreateCustomerRequest = createCustomerRequest;  
        }
    }
}
