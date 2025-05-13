using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;

namespace Barber.Colocho.Domain.Interface.User
{
    public interface IUserDomain
    {
        Task<ResponseDomain<bool>> InsertUser(UserInsertDomainDto dto);
    }
}
