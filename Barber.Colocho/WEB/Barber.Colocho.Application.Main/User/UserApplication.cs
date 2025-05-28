using AutoMapper;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;

namespace Barber.Colocho.Application.Main.User
{
    public class UserApplication : IUserApplication
    {

        #region Constructor
        private readonly IUserDomain userDomain;
        public readonly IMapper mapper;

        public UserApplication(IUserDomain userDomain, IMapper mapper)
        {
            this.userDomain = userDomain;
            this.mapper = mapper;
        }
        #endregion

        public async Task<ResponseApplication<bool>> DeleteAccount(RequestApplication<Guid> request)
        {
            if (request == null)
                return new ResponseApplication<bool>
                {
                    Result = false,
                    Message = "object null"
                };

            if (request.Request == Guid.Empty)
                return new ResponseApplication<bool>
                {
                    Result = false,
                    Message = "Object null request"
                };
            var requestDomain = mapper.Map<RequestDomain<Guid>>(request);
            var responseDomain = await userDomain.DeleteAccount(requestDomain);
            var responseApplication = mapper.Map<ResponseApplication<bool>>(responseDomain);
            return responseApplication;
        }

        public async Task<ResponseApplication<UserApplicationDto>> GetUserByIdAsync(RequestApplication<Guid> request)
        {
            var mapRequestDomain = mapper.Map<RequestDomain<Guid>>(request);
            var getUser = await userDomain.GetUserByIdAsync(mapRequestDomain);
            var responseApplication = mapper.Map<ResponseApplication<UserApplicationDto>>(getUser);
            return responseApplication;
        }       
    }
}
