using Asp.Versioning;
using Barber.Colocho.Core.Auth;
using Barber.Colocho.Models.Authenticate;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class OAuthController : ControllerBase
    {
        private readonly AuthenticateBL authenticate;

        public OAuthController(AuthenticateBL authenticate)
        {
            this.authenticate = authenticate;
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel command)
        {
            var result = await authenticate.RefreshToken(command);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] TokenModel command)
        {
            var result = await authenticate.Token(command);
            return (result != null && result.Result != null)?Ok(result) : BadRequest(result);
        }
    }
}
