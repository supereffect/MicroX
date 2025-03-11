using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSWebApiProject.Business.DTO.Response;
using MediatR;

namespace CQRSWebApiProject.Business.Queries
{
    public class GetCustomerByIDQuery : IRequest<GetCustomerByIdQueryResponse>
    {
        public int Id { get; set; } 
        public GetCustomerByIDQuery(int id)
        {
            this.Id = id;
        }
    }
}
