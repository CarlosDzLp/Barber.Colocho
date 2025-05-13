using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;

namespace Barber.Colocho.Domain.Core.User
{
    public class UserDomain : IUserDomain
    {
        public Task<ResponseDomain<bool>> InsertUser(UserInsertDomainDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
