using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Application.Interface.User
{
    public interface IUserApplication
    {
        Task<ResponseApplication<UserApplicationDto>> GetUserEmailAsync(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserPasswordAsync(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserPhoneAsync(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserByIdAsync(RequestApplication<Guid> request);
        Task<ResponseApplication<bool>> InsertUserAsync(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<bool>> DeleteUserAsync(RequestApplication<Guid> request);
        Task<ResponseApplication<bool>> UpdateUserAsync(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<bool>> UpdateImageUserAsync(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<IEnumerable<UserApplicationDto>>> GetAllUsersAsync(Expression<Func<UserApplicationDto, bool>>? filter = null);
    }
}
