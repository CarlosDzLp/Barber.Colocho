using AutoMapper;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.DTO.Version;
using Barber.Colocho.Application.Interface.Response;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Domain.Interface.Response;
using Barber.Colocho.Domain.Interface.User;

namespace Barber.Colocho.Application.Main.User
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserDomain userDomain;
        private readonly IMapper mapper;
        #region Constructor
        public UserApplication(IUserDomain userDomain,IMapper mapper)
        {
            this.userDomain = userDomain;
            this.mapper = mapper;
        }
        #endregion

        public async Task<ResponseApplication<bool>> InsertUser(UserInsertApplicationDto dto)
        {
            try
            {
                var map = mapper.Map<ResponseDomain<>>
                var result = await userDomain.InsertUser()
                var map = mapper.Map<ResponseApplication<VersionApplicationDto>>(result);
                return map;
            }
            catch (Exception ex)
            {
                return new ResponseApplication<VersionApplicationDto>();
            }
        }
    }
}
