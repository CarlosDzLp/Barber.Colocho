using Asp.Versioning;
using Barber.Colocho.Core.Plan;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    [AllowAnonymous]
    public class PlanController : ControllerBase
    {

        #region Constructor
        private readonly PlanBL plan;
        public PlanController(PlanBL plan)
        {
            this.plan = plan;
        }
        #endregion

        [HttpGet("GetPlan")]
        public async Task<IActionResult> GetPlan()
        {
            var result =await plan.ListPlan();
            return (result != null && result.Result != null && result.Result.Count > 0) ? Ok(result) : BadRequest(result);
        }
    }
}
