using AutoMapper;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.Business.Queries;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomerQueryResponse>>
    {

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;
        private readonly IMapper _mapper;



        public GetAllCustomersQueryHandler(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetList();
            if (response.Count > 0)
            {
                return _mapper.Map<List<GetAllCustomerQueryResponse>>(response);
            }

            throw new System.Exception("Customer Not Found!");
        }

    }
}
