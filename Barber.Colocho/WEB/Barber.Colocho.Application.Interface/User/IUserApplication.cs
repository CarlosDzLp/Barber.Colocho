using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;

namespace Barber.Colocho.Application.Interface.User
{
    public interface IUserApplication
    {
        Task<ResponseApplication<UserApplicationDto>> GetUserByIdAsync(RequestApplication<Guid> request);
        Task<ResponseApplication<bool>> DeleteAccount(RequestApplication<Guid> request);
    }
}
