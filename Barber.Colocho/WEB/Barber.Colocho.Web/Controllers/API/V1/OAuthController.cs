using Asp.Versioning;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Application.Main.Modules;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class OAuthController : ControllerBase
    {

        #region Constructor
        private readonly IUserApplication userApplication;
        public OAuthController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }
        #endregion

        [HttpPost("RefreshToken/Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] UserApplicationDto command)
        {
            var result = await userApplication.DeleteUser(new Application.Interface.Response.RequestApplication<UserApplicationDto>() { Request = command });
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        //[HttpPost("Authenticate/Oauth")]
        //public async Task<IActionResult> Authenticate([FromBody] TokenModel command)
        //{
        //    var result = await authenticate.Token(command);
        //    return (result != null && result.Result != null)?Ok(result) : BadRequest(result);
        //}
    }
}
