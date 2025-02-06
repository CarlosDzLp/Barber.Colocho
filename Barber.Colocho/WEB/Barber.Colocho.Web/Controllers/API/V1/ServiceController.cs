using Asp.Versioning;
using Barber.Colocho.Core.Service;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Models.Service;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Colocho.Web.Controllers.API.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionFilter]
    public class ServiceController : ControllerBase
    {
        #region Constructor
        private readonly ServiceBL service;
        public ServiceController(ServiceBL service)
        {
            this.service = service;
        }
        #endregion

        [Authorize]
        [HttpPost("{UserId}/Company/{CompanyId}/AddService")]
        public async Task<IActionResult> AddService(int UserId, int CompanyId, [FromBody] AddServiceModel model)
        {
            var result = await service.AddService(UserId, CompanyId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/Service/{ServiceId}/DeleteService")]
        public async Task<IActionResult> DeleteService(int UserId,int CompanyId, int ServiceId)
        {
            var result = await service.DeleteService(ServiceId, CompanyId, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Service/{ServiceId}/ImageService/{ImageServiceId}/DeleteImageService")]
        public async Task<IActionResult> DeleteImageService(int UserId,int ServiceId, int ImageServiceId)
        {
            var result = await service.DeleteImageService(ImageServiceId, ServiceId, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/Company/{CompanyId}/Service/{ServceId}/AddImageService")]
        public async Task<IActionResult> AddImageService(int UserId,int CompanyId, int ServceId, [FromForm] AddImageModel model)
        {
            var result = await service.AddImageService(ServceId, CompanyId, model, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/Company/{CompanyId}/Service/{ServiceId}/UpdateService")]
        public async Task<IActionResult> UpdateService(int UserId, int CompanyId, int ServiceId, [FromBody] UpdateServiceModel model)
        {
            var result = await service.UpdateService(ServiceId, CompanyId, model, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/GetListServiceByIdCompany")]
        public async Task<IActionResult> GetListServiceByIdCompany(int UserId, int CompanyId)
        {
            var result = await service.GetListServiceByIdCompany(CompanyId, UserId);
            return (result != null && result.Result != null && result.Result.Count > 0) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/Service/{ServiceId}/GetServiceByIdService")]
        public async Task<IActionResult> GetServiceByIdService(int UserId, int CompanyId, int ServiceId)
        {
            var result = await service.GetServiceByIdService(CompanyId, ServiceId, UserId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/Company/{CompanyId}/Service/{ServiceId}/AddCalificationService")]
        public async Task<IActionResult> AddCalificationService(int UserId, int CompanyId, int ServiceId, [FromBody] AddRatingModel model)
        {
            var result = await service.AddCalificationService(CompanyId, ServiceId, UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        #region Service All User
        [AllowAnonymous]
        [HttpGet("GetListServiceAllUser/{Page}")]
        public async Task<IActionResult> GetListServiceAllUser(int Page)
        {
            var result = await service.GetListServiceAllUser(Page);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("{CompanyId}/Service/{ServiceId}/GetServiceAllUser")]
        public async Task<IActionResult> GetServiceAllUser(int CompanyId, int ServiceId)
        {
            var result = await service.GetServiceAllUser(CompanyId, ServiceId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("{ServiceId}/ListCalificationService")]
        public async Task<IActionResult> ListCalificationService(int ServiceId)
        {
            var result = await service.ListCalificationService(ServiceId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }
        #endregion
    }
}
