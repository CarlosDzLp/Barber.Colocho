using Barber.Colocho.Application.DTO.Version;
using Barber.Colocho.Application.Interface.Response;

namespace Barber.Colocho.Application.Interface.Version
{
    public interface IVersionApplication
    {
        Task<ResponseApplication<VersionApplicationDto>> LastVersion();
    }
}
