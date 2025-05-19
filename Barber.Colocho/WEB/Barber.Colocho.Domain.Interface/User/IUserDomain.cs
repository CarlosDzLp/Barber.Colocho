using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Domain.Interface.User
{
    public interface IUserDomain
    {
        Task<ResponseDomain<UserDomainDto>> GetUserEmail(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserPassword(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserPhone(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserById(RequestDomain<Guid> request);
        Task<ResponseDomain<bool>> InsertUser(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<bool>> DeleteUser(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<bool>> UpdateUser(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<bool>> UpdateImageUser(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<IEnumerable<UserDomainDto>>> GetAllUsers(Expression<Func<UserDomainDto, bool>>? filter = null);
    }
}
