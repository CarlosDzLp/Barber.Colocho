using Barber.Colocho.Infraestructure.Repository.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Infraestructure.Repository.User
{
    public interface IUserInfraestructure
    {
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserEmail(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>>GetUserPassword(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserPhone(RequestInfraestructure<string> request);
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserById(RequestInfraestructure<Guid> request);
        Task<ResponseInfraestructure<bool>> InsertUser(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<bool>> DeleteUser(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<bool>> UpdateUser(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<bool>> UpdateImageUser(RequestInfraestructure<Data.Tables.User> request);
        Task<ResponseInfraestructure<IEnumerable<Data.Tables.User>>> GetAllUsers(Expression<Func<Data.Tables.User, bool>>? filter = null);
    }
}
