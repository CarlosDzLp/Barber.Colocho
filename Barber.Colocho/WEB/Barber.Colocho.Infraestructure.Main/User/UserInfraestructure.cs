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

        public async Task<ResponseInfraestructure<bool>> DeleteUserAsync(RequestInfraestructure<Guid> request)
        {
            var us = await user.FindAsync(c => c.Id == request.Request);
            us.Deleted = DateTime.UtcNow;
            us.Updated = DateTime.UtcNow;
            us.IsActive = false;
            await user.UpdateAsync(us);
            return new ResponseInfraestructure<bool>
            {
                Result = true,
                Count = 1,
                Message = null
            };
        }

        public async Task<ResponseInfraestructure<IEnumerable<Data.Tables.User>>> GetAllUsersAsync(Expression<Func<Data.Tables.User, bool>>? filter = null)
        {
            var userAll = await user.GetAllAsync(filter);
            return new ResponseInfraestructure<IEnumerable<Data.Tables.User>>
            {
                Result = userAll,
                Count = userAll.ToList().Count,
                Message = null
            };
        }

        public async Task<ResponseInfraestructure<Data.Tables.User>> GetUserByIdAsync(RequestInfraestructure<Guid> request)
        {
            var us = await user.FindAsync(c => c.Id == request.Request);
            return new ResponseInfraestructure<Data.Tables.User>
            {
                Result = us,
                Count = (us != null ? 1 : 0)
            };
        }

        public async Task<ResponseInfraestructure<Data.Tables.User>> GetUserEmailAsync(RequestInfraestructure<string> request)
        {
            var us = await user.FindAsync(c => c.Email == request.Request);
            return new ResponseInfraestructure<Data.Tables.User>
            {
                Result = us,
                Count = (us != null) ? 1 : 0
            };
        }

        public async Task<ResponseInfraestructure<Data.Tables.User>> GetUserPasswordAsync(RequestInfraestructure<string> request)
        {
            var us = await user.FindAsync(c => c.Pass == request.Request);
            return new ResponseInfraestructure<Data.Tables.User>
            {
                Result = us,
                Count = (us != null) ? 1 : 0
            };
        }

        public async Task<ResponseInfraestructure<Data.Tables.User>> GetUserPhoneAsync(RequestInfraestructure<string> request)
        {
            var us = await user.FindAsync(c => c.Phone == request.Request);
            return new ResponseInfraestructure<Data.Tables.User>
            {
                Result = us,
                Count = (us != null) ? 1 : 0
            };
        }

        public async Task<ResponseInfraestructure<bool>> InsertUserAsync(RequestInfraestructure<Data.Tables.User> request)
        {
            var us = await user.AddAsync(request.Request);
            return new ResponseInfraestructure<bool>
            {
                Result = (us.Id != null),
                Count = (us != null) ? 1 : 0
            };
        }

        public async Task<ResponseInfraestructure<bool>> UpdateImageUserAsync(RequestInfraestructure<Data.Tables.User> request)
        {
            var us = await user.FindAsync(c => c.Id == request.Request.Id);
            if(us != null)
            {
                us.Image = request.Request.Image;
                await user.UpdateAsync(us);
            }
            return new ResponseInfraestructure<bool>
            {
                Result = true,
                Count = 1
            };
        }

        public async Task<ResponseInfraestructure<bool>> UpdateUserAsync(RequestInfraestructure<Data.Tables.User> request)
        {
            var us = await user.FindAsync(c => c.Id == request.Request.Id);
            if (us != null)
            {
                us = request.Request;
                await user.UpdateAsync(us);
            }
            return new ResponseInfraestructure<bool>
            {
                Result = true,
                Count = 1
            };
        }
    }
}
