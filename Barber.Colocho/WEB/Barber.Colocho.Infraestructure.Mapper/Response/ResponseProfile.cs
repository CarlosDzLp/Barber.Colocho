using AutoMapper;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Domain.Entity.Version;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Infraestructure.Repository.Response;

namespace Barber.Colocho.Transversal.Mapper.Response
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap(typeof(ResponseInfraestructure<>), typeof(ResponseDomain<>));
            CreateMap(typeof(ResponseDomain<>), typeof(ResponseApplication<>)).ReverseMap();
            CreateMap(typeof(RequestInfraestructure<>), typeof(RequestDomain<>)).ReverseMap();
            CreateMap(typeof(RequestDomain<>), typeof(RequestApplication<>)).ReverseMap();
        }
    }
}
