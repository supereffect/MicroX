using AutoMapper;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.Business.Queries;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Handlers
{
    public class GetCustomerByIdHandlers : IRequestHandler<GetCustomerByIDQuery, GetCustomerByIdQueryResponse>
    {

        private readonly IGenericRepository<Entity.Concrete.Customer> repository;
        private readonly IMapper mapper;


        public GetCustomerByIdHandlers(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<GetCustomerByIdQueryResponse> Handle(GetCustomerByIDQuery request, CancellationToken cancellationToken)
        {
            var response = await repository.Get(x => x.Id == request.Id);
            if (response != null)
            {
                return mapper.Map<GetCustomerByIdQueryResponse>(response);
            }

            throw new System.Exception("Customer Not Found!");
        }
    }
}
