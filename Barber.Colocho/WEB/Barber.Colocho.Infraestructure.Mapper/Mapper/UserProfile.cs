using AutoMapper;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Domain.Entity.User;

namespace Barber.Colocho.Transversal.Mapper.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Infraestructure.Data.Tables.User, UserDomainDto>().ReverseMap();
            CreateMap<UserDomainDto,UserApplicationDto>().ReverseMap();
        }
    }
}
