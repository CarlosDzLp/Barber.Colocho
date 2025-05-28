using Barber.Colocho.Infraestructure.Repository.Response;
using System.Linq.Expressions;

namespace Barber.Colocho.Infraestructure.Repository.User
{
    public interface IUserInfraestructure
    {
        Task<ResponseInfraestructure<Data.Tables.User>> GetUserByIdAsync(RequestInfraestructure<Guid> request);
        Task<ResponseInfraestructure<bool>> DeleteAccount(RequestInfraestructure<Data.Tables.User> request);
    }
}
