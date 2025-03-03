using CQRSWebApiProject.Business.DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Queries
{
    public class GetAllCustomersQuery : IRequest<List<GetAllCustomerQueryResponse>>
    {
    }
}
