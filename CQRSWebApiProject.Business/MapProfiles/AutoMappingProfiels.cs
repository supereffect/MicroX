using AutoMapper;
using CQRSWebApiProject.Business.DTO.Request;
using CQRSWebApiProject.Business.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.Business.MapProfiles
{
    public class AutoMappingProfiels : Profile
    {
        public AutoMappingProfiels()
        {

            CreateMap<Entity.Concrete.Customer, CreateCustomerRequest>().ReverseMap();
            CreateMap<Entity.Concrete.Customer, CreateCustomerResponse>().ReverseMap();
            ////kural belirtmek isterilirse aşağıdaki gibi eklenebilir diye burayı koyuyorum ilgilenenler için<
            //   CreateMap<GrpcPlatformModel, Platform>().ForMember(x => x.ExternalId, opt => opt.MapFrom(src => src.PlatformId))// except externalıd=>platformid mapping are not necessary but, its just for trus issues :):):
            //       .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            //       .ForMember(dest => dest.Commands, opt => opt.Ignore());

        }
    }
}

