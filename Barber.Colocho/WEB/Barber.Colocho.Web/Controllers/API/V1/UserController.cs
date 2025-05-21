using Asp.Versioning;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Application.Main.Modules;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class UserController : ControllerBase
    {
        #region Constructor
        private readonly IUserApplication userApplication;
        public UserController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }
        #endregion

        [HttpDelete("{IdUser}/DeleteUserAsync")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute(Name = "IdUser")] Guid IdUser)
        {
            var result = await userApplication.DeleteUserAsync(new Application.Interface.Response.RequestApplication<Guid> { Request = IdUser });
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }
    }
}
