using Asp.Versioning;
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
        //[HttpPost("RefreshToken/Refresh")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel command)
        //{
        //    var result = await authenticate.RefreshToken(command);
        //    return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        //}

        //[HttpPost("Authenticate/Oauth")]
        //public async Task<IActionResult> Authenticate([FromBody] TokenModel command)
        //{
        //    var result = await authenticate.Token(command);
        //    return (result != null && result.Result != null)?Ok(result) : BadRequest(result);
        //}
    }
}
