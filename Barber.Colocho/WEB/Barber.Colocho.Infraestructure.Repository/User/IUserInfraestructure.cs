using Barber.Colocho.Infraestructure.Repository.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Infraestructure.Repository.User
{
    public interface IUserInfraestructure
    {
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserEmailAsync(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserPasswordAsync(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserPhoneAsync(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserByIdAsync(RequestInfraestructure<Guid> request);
        Task<ResponseInfraestructure<bool>> InsertUserAsync(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<bool>> DeleteUserAsync(RequestInfraestructure<Guid> request);
        Task<ResponseInfraestructure<bool>> UpdateUserAsync(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<bool>> UpdateImageUserAsync(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<IEnumerable<Data.Tables.User>>> GetAllUsersAsync(Expression<Func<Data.Tables.User, bool>>? filter = null);
    }
}
