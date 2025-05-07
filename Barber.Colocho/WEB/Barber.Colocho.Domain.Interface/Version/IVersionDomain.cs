using Barber.Colocho.Domain.Entity.Version;
using Barber.Colocho.Domain.Interface.Response;

namespace Barber.Colocho.Domain.Interface.Version
{
    public interface IVersionDomain
    {
        Task<ResponseDomain<VersionDomainDto>> LastVersion();
    }
}
