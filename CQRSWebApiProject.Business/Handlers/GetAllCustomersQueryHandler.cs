using AutoMapper;
using CQRSWebApiProject.Business.DTO.Response;
using CQRSWebApiProject.Business.Queries;
using CQRSWebApiProject.DAL.Concrete.EntityFramework.GenericRepository;
using CQRSWebApiProject.DAL.Concrete.Redis.Abstract;
using CQRSWebApiProject.DAL.Concrete.Redis.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomerQueryResponse>>
    {

        private readonly IGenericRepository<Entity.Concrete.Customer> repository;
        private readonly IMapper mapper;
        private readonly IRedisService redisService;


        public GetAllCustomersQueryHandler(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper, IRedisService redisService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.redisService = redisService;
        }



        public async Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // Redis'ten doğru anahtarı kullanarak veri çekiliyor
            var test = await redisService.GetValueAsync("test");

            // Eğer `test` değeri Redis'ten başarılı bir şekilde gelirse, "test" anahtarının değeri konsola yazdırılabilir
            Console.WriteLine(test); // Bu satır sadece test amacıyla eklenmiştir

            var response = await repository.GetList();
            if (response.Count > 0)
            {
                return mapper.Map<List<GetAllCustomerQueryResponse>>(response);
            }

            throw new System.Exception("Customer Not Found!");
        }

    }
}
