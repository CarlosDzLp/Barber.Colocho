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

        public async Task<ResponseDomain<bool>> DeleteUserAsync(RequestDomain<Guid> request)
        {
            if (request == null)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Count = 0,
                    Message = "Campos vacios"
                };
            if (request.Request == Guid.Empty)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Count = 0,
                    Message = "Usuario no encontrado"
                };


            var mapRequest = mapper.Map<RequestInfraestructure<Guid>>(request);
            var us = await userInfraestructure.GetUserByIdAsync(mapRequest);
            if (us != null && us.Result == null)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Message = "Usuario no encontrado"
                };


            var result = await userInfraestructure.DeleteUserAsync(mapRequest);
            var mapResponse = mapper.Map<ResponseDomain<bool>>(result);
            return mapResponse;
        }

        public Task<ResponseDomain<IEnumerable<UserDomainDto>>> GetAllUsersAsync(Expression<Func<UserDomainDto, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserByIdAsync(RequestDomain<Guid> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserEmailAsync(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserPasswordAsync(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<UserDomainDto>> GetUserPhoneAsync(RequestDomain<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> InsertUserAsync(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> UpdateImageUserAsync(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDomain<bool>> UpdateUserAsync(RequestDomain<UserDomainDto> request)
        {
            throw new NotImplementedException();
        }
    }
}
