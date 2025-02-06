using Asp.Versioning;
using Barber.Colocho.Core.Company;
using Barber.Colocho.Models.Company;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    [Authorize]
    public class CompanyController : ControllerBase
    {

        #region Constructor
        private readonly CompanyBL companyBL;
        public CompanyController(CompanyBL companyBL)
        {
            this.companyBL = companyBL;
        }
        #endregion

        [HttpPost("{UserId}/AddCompany")]
        public async Task<IActionResult> AddCompany(int UserId, [FromBody] AddCompanyModel model)
        {
            var result = await companyBL.AddCompany(model, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/Company/{CompanyId}/SuscripcionActiveCompany")]
        public async Task<IActionResult> SuscripcionActiveCompany(int UserId, int CompanyId)
        {
            var result = await companyBL.SuscripcionActiveCompany(UserId, CompanyId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/DeleteCompany/{CompanyId}")]
        public async Task<IActionResult> DeleteCompany(int UserId, int CompanyId)
        {
            var result = await companyBL.DeleteCompany(CompanyId, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/GetListCompany")]
        public async Task<IActionResult> GetListCompany(int UserId)
        {
            var result = await companyBL.GetListCompany(UserId);
            return (result != null && result.Result != null && result.Result.Count > 0) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/GetCompany/{CompanyId}")]
        public async Task<IActionResult> GetCompany(int UserId, int CompanyId)
        {
            var result = await companyBL.GetCompany(CompanyId, UserId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/UpdateCompany/{CompanyId}")]
        public async Task<IActionResult> UpdateCompany(int UserId, int CompanyId, [FromBody] UpdateCompanyModel model)
        {
            var result = await companyBL.UpdateCompany(CompanyId, UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/AddImageCompany/{CompanyId}")]
        public async Task<IActionResult> AddImageCompany(int UserId, int CompanyId, [FromForm] AddImageModel model)
        {
            var result = await companyBL.AddImageCompany(CompanyId, UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/Company/{CompanyId}/DeleteImageCompany/{ImageId}")]
        public async Task<IActionResult> DeleteImageCompany(int UserId, int CompanyId, int ImageId)
        {
            var result = await companyBL.DeleteImageCompany(ImageId, UserId, CompanyId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }
    }
}
