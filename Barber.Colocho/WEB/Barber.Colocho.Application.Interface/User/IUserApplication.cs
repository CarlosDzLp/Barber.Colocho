using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Application.Interface.User
{
    public interface IUserApplication
    {
        Task<ResponseApplication<UserApplicationDto>> GetUserEmail(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserPassword(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserPhone(RequestApplication<string> request);
        Task<ResponseApplication<UserApplicationDto>> GetUserById(RequestApplication<Guid> request);
        Task<ResponseApplication<bool>> InsertUser(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<bool>> DeleteUser(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<bool>> UpdateUser(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<bool>> UpdateImageUser(RequestApplication<UserApplicationDto> request);
        Task<ResponseApplication<IEnumerable<UserApplicationDto>>> GetAllUsers(Expression<Func<UserApplicationDto, bool>>? filter = null);
    }
}
