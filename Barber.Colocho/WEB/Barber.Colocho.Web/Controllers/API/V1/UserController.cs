using Asp.Versioning;
using Barber.Colocho.Application.DTO.User;
using Barber.Colocho.Application.Interface.User;
using Barber.Colocho.Application.Main.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication userApplication;
        #region Constructor
        public UserController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }
        #endregion

        [HttpPost("InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertApplicationDto user)
        {
            var result = await userApplication.InsertUser(user);
            return Ok(result);
        }
    }
}
