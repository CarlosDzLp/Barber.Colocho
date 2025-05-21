using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Domain.Interface.User
{
    public interface IUserDomain
    {
        Task<ResponseDomain<UserDomainDto>> GetUserEmailAsync(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserPasswordAsync(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserPhoneAsync(RequestDomain<string> request);
        Task<ResponseDomain<UserDomainDto>> GetUserByIdAsync(RequestDomain<Guid> request);
        Task<ResponseDomain<bool>> InsertUserAsync(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<bool>> DeleteUserAsync(RequestDomain<Guid> request);
        Task<ResponseDomain<bool>> UpdateUserAsync(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<bool>> UpdateImageUserAsync(RequestDomain<UserDomainDto> request);
        Task<ResponseDomain<IEnumerable<UserDomainDto>>> GetAllUsersAsync(Expression<Func<UserDomainDto, bool>>? filter = null);
    }
}
