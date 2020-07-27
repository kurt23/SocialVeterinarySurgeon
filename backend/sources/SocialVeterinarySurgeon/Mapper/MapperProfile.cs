using AutoMapper;
using SocialVeterinarySurgeon.Domain.Entities;
using SocialVeterinarySurgeon.Dto;

namespace SocialVeterinarySurgeon.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, Employee>();
            CreateMap<Pet, Pet>();
            CreateMap<Pet, PetDto>().ReverseMap();
        }
    }
}
