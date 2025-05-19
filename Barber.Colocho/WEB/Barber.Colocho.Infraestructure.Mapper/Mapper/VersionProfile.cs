using AutoMapper;
using Barber.Colocho.Application.DTO.Version;
using Barber.Colocho.Domain.Entity.Version;

namespace Barber.Colocho.Transversal.Mapper.Mapper
{
    public class VersionProfile : Profile
    {
        public VersionProfile() 
        {
            CreateMap<Infraestructure.Data.Tables.Version, VersionDomainDto>().ReverseMap();
            CreateMap<VersionDomainDto, VersionApplicationDto>().ReverseMap();
        }
    }
}
