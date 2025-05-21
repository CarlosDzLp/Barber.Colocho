using AutoMapper;
using Barber.Colocho.Application.DTO.Version;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Application.Interface.Version;
using Barber.Colocho.Domain.Interface.Version;

namespace Barber.Colocho.Application.Main.Version
{
    public class VersionApplication : IVersionApplication
    {
        #region Constructor
        private readonly IVersionDomain versionDomain;
        private readonly IMapper mapper;
        public VersionApplication(IVersionDomain versionDomain, IMapper mapper)
        {
            this.versionDomain = versionDomain;
            this.mapper = mapper;
        }
        #endregion
        public async Task<ResponseApplication<VersionApplicationDto>> LastVersion()
        {
            try
            {
                var result = await versionDomain.LastVersion();
                var map = mapper.Map<ResponseApplication<VersionApplicationDto>>(result);
                return map;
            }
            catch (Exception ex)
            {
                return new ResponseApplication<VersionApplicationDto>();
            }
        }
    }
}
