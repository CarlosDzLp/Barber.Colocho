using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;

namespace Barber.Colocho.Application.Interface.User
{
    public interface IUserApplication
    {
        Task<ResponseApplication<bool>> InsertUser(UserInsertApplicationDto dto);
    }
}
