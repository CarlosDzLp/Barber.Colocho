using Barber.Colocho.Infraestructure.Repository.Repository;
using Barber.Colocho.Infraestructure.Repository.Response;
using Barber.Colocho.Infraestructure.Repository.User;
using System.Linq.Expressions;

namespace Barber.Colocho.Infraestructure.Main.User
{
    public class UserInfraestructure : IUserInfraestructure
    {
        #region Constructor
        private readonly IGenericRepository<Data.Tables.User> user;
        public UserInfraestructure(IGenericRepository<Data.Tables.User> user)
        {
            this.user = user;
        }
        #endregion

        public async Task<ResponseInfraestructure<bool>> DeleteAccount(RequestInfraestructure<Data.Tables.User> request)
        {
            var us = request.Request;
            us.IsDeletedAccount = true;
            await user.UpdateAsync(us);
            return new ResponseInfraestructure<bool>
            {
                Result = true,
                IsSuccess = true,
                Message = "Se ha eliminado la cuenta"
            };
        }

        public async Task<ResponseInfraestructure<Data.Tables.User>> GetUserByIdAsync(RequestInfraestructure<Guid> request)
        {
            var us = await user.FindAsync(c => c.Id == request.Request);
            return new ResponseInfraestructure<Data.Tables.User>
            {
                Result = us,
                IsSuccess = us != null,
                Message = (us is null) ? "No se encontro el usuario" : string.Empty
            };
        }
    }
}
