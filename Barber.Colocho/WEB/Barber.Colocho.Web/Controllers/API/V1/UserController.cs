using Asp.Versioning;
using Barber.Colocho.Application.Interface.Response;
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

        [HttpDelete("{IdUser}/DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromRoute(Name = "IdUser")]Guid IdUser)
        {
            var result = await userApplication.DeleteAccount(new Application.Interface.Response.RequestApplication<Guid> { Request = IdUser });
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{IdUser}/GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute(Name = "IdUser")]Guid IdUser)
        {
            var result = await userApplication.GetUserByIdAsync(new RequestApplication<Guid> { Request = IdUser});
            return (result.IsSuccess) ? Ok(result) : BadRequest(result);
        }
    }
}
