using AutoMapper;
using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;
using Barber.Colocho.Infraestructure.Repository.Response;
using Barber.Colocho.Infraestructure.Repository.User;
using System.Linq.Expressions;

namespace Barber.Colocho.Domain.Core.User
{
    public class UserDomain : IUserDomain
    {

        #region Constructor
        private readonly IUserInfraestructure userInfraestructure;
        private readonly IMapper mapper;

        public UserDomain(IUserInfraestructure userInfraestructure,IMapper mapper)
        {
            this.userInfraestructure = userInfraestructure;
            this.mapper = mapper;
        }
        #endregion

        public async Task<ResponseDomain<bool>> DeleteUser(RequestDomain<UserDomainDto> request)
        {
            if (request == null)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Count = 0,
                    Message = "Campos vacios"
                };
            if (request.Request.Id == null)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Count = 0,
                    Message = "Usuario no encntrado"
                };
                var mapRequest = mapper.Map<RequestInfraestructure<Infraestructure.Data.Tables.User>>(request);
                var result = await userInfraestructure.DeleteUser(mapRequest);
                var mapResponse = mapper.Map<ResponseDomain<bool>>(result);
                return mapResponse;
        }

        public Task<ResponseDomain<IEnumerable<UserDomainDto>>> GetAllUsers(Expression<Func<UserDomainDto, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserById(RequestDomain<Guid> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserEmail(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserPassword(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserPhone(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> InsertUser(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> UpdateImageUser(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> UpdateUser(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }
    }
}
