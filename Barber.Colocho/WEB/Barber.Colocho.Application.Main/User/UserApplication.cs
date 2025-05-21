using AutoMapper;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;
using Barber.Colocho.Transversal.Validations.Extensions;
using FluentValidation;
using System.Linq.Expressions;

namespace Barber.Colocho.Application.Main.User
{
    public class UserApplication : IUserApplication
    {

        #region Constructor
        private readonly IUserDomain userDomain;
        public readonly IMapper mapper;
        private readonly IValidator<UserApplicationDto> userValidator;

        public UserApplication(IUserDomain userDomain, IMapper mapper, IValidator<UserApplicationDto> userValidator)
        {
            this.userDomain = userDomain;
            this.mapper = mapper;
            this.userValidator = userValidator;
        }       
        #endregion

        public async Task<ResponseApplication<bool>> DeleteUser(RequestApplication<UserApplicationDto> request)
        {
            var validationResult = await userValidator.ValidateAsStringAsync(request.Request);
            if (!string.IsNullOrWhiteSpace(validationResult))
                return new ResponseApplication<bool>
                {
                    Result = false,
                    Message = validationResult
                };

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
