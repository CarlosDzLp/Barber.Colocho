using AutoMapper;
using Barber.Colocho.Domain.Entity.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;
using Barber.Colocho.Infraestructure.Repository.Response;
using Barber.Colocho.Infraestructure.Repository.User;

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

        public async Task<ResponseDomain<bool>> DeleteAccount(RequestDomain<Guid> request)
        {
            var mapRequest = mapper.Map<RequestInfraestructure<Guid>>(request);
            var searchUser = await userInfraestructure.GetUserByIdAsync(mapRequest);
            if (!searchUser.IsSuccess)
                return new ResponseDomain<bool>
                {
                    Result = false,
                    Message = "Usuario no encontrado"
                };

            var requestInfra = new RequestInfraestructure<Infraestructure.Data.Tables.User>()
            {
                Request = searchUser.Result
            };
            var responseInfraestructure = await userInfraestructure.DeleteAccount(requestInfra);
            var response = mapper.Map<ResponseDomain<bool>>(responseInfraestructure);
            return response;
        }

        public async Task<ResponseDomain<UserDomainDto>> GetUserByIdAsync(RequestDomain<Guid> request)
        {
            if (request.Request == Guid.Empty)
                return new ResponseDomain<UserDomainDto>
                {
                    Result = null,
                    IsSuccess = false,
                    Message ="Ingrese el Id del usuario"
                };

            var requestInfra = mapper.Map<RequestInfraestructure<Guid>>(request);
            var getUser = await userInfraestructure.GetUserByIdAsync(requestInfra);
            var responseDomain = mapper.Map<ResponseDomain<UserDomainDto>>(getUser);
            return responseDomain; 
        }
    }
}
