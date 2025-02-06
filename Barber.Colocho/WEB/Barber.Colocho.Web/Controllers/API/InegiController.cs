using Asp.Versioning;
using Barber.Colocho.Core.INEGI;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class InegiController : ControllerBase
    {

        #region Constructor
        private readonly InegiBL inegi;
        public InegiController(InegiBL inegi)
        {
            this.inegi = inegi;
        }
        #endregion

        [HttpGet("GetState")]
        public async Task<IActionResult> GetState()
        {
            var result = await inegi.GetState();
            return result != null && result.Result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetCity")]
        public async Task<IActionResult> GetCity([FromQuery(Name = "IdState")] int IdState)
        {
            var result = await inegi.GetCity(IdState);
            return result != null && result.Result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetColony")]
        public async Task<IActionResult> GetColony([FromQuery(Name = "IdCity")] int IdCity)
        {
            var result = await inegi.GetColony(IdCity);
            return result != null && result.Result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetCodePostal")]
        public async Task<IActionResult> GetCodePostal([FromQuery(Name = "CodePostal")] string CodePostal)
        {
            var result = await inegi.GetCodePostal(CodePostal);
            return result != null && result.Result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetCodePostalInsert")]
        public async Task<IActionResult> GetCodePostalInsert()
        {
            var result = await inegi.GetCodePostalInsert();
            return result != null && result.Result ? Ok(result) : BadRequest(result);
        }
    }
}
