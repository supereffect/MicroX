using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CQRSWebApiProject.Business.Queries
{
    public class GetCustomerByIDQuery : IRequest<Get>
    {
    }
}
