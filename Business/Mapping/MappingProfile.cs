using AutoMapper;
using Business.Dtos;
using Core.Entites;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer <-> CustomerDto (Çift yönlü eşleme)
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
