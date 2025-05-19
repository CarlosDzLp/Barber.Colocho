using AutoMapper;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;
using System.Linq.Expressions;

namespace Barber.Colocho.Application.Main.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserDomain userDomain;
        #region Constructor
        public readonly IMapper mapper;
        public UserApplication(IUserDomain userDomain, IMapper mapper)
        {
            this.userDomain = userDomain;
            this.mapper = mapper;
        }

        
        #endregion

        public async Task<ResponseApplication<bool>> DeleteUser(RequestApplication<UserApplicationDto> request)
        {
            var requestDomain = mapper.Map<RequestDomain<Domain.Entity.User.UserDomainDto>>(request);
            var responseDomain = await userDomain.DeleteUser(requestDomain);
            var responseApplication = mapper.Map<ResponseApplication<bool>>(responseDomain);
            return responseApplication;
        }

        public Task<ResponseApplication<IEnumerable<UserApplicationDto>>> GetAllUsers(Expression<Func<UserApplicationDto, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<UserApplicationDto>> GetUserById(RequestApplication<Guid> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<UserApplicationDto>> GetUserEmail(RequestApplication<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<UserApplicationDto>> GetUserPassword(RequestApplication<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<UserApplicationDto>> GetUserPhone(RequestApplication<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<bool>> InsertUser(RequestApplication<UserApplicationDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<bool>> UpdateImageUser(RequestApplication<UserApplicationDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseApplication<bool>> UpdateUser(RequestApplication<UserApplicationDto> request)
        {
            throw new NotImplementedException();
        }
    }
}
