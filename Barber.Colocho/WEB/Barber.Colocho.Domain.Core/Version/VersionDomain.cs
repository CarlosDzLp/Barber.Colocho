using AutoMapper;
using Barber.Colocho.Domain.Entity.Version;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.Version;
using Barber.Colocho.Infraestructure.Repository.Version;

namespace Barber.Colocho.Domain.Core.Version
{
    public class VersionDomain : IVersionDomain
    {
        private readonly IVersionInfraestructure versionInfraestructure;
        private readonly IMapper mapper;
        #region Constuctor
        public VersionDomain(IVersionInfraestructure versionInfraestructure, IMapper mapper)
        {
            this.versionInfraestructure = versionInfraestructure;
            this.mapper = mapper;
        }
        #endregion

        public async Task<ResponseDomain<VersionDomainDto>> LastVersion()
        {
            var response = await versionInfraestructure.LastVersion();
            var result = mapper.Map<ResponseDomain<VersionDomainDto>>(response);
            return result;
        }
    }
}
