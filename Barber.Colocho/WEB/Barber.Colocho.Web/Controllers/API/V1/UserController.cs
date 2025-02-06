using Asp.Versioning;
using Barber.Colocho.Core.User;
using Barber.Colocho.Models.Code;
using Barber.Colocho.Models.ForgotPassword;
using Barber.Colocho.Models.Generic;
using Barber.Colocho.Models.User;
using Barber.Colocho.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserBL user;
        public UserController(UserBL user)
        {
            this.user = user;
        }
        #endregion

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
        {
            var result = await user.AddUserAsync(model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("{UserId}/SendCodeUser")]
        public async Task<IActionResult>SendCodeUser(int UserId, [FromBody] CodeModel code)
        {
            var result = await user.SendCodeUser(code, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("{UserId}/ResendCodeUser")]
        public async Task<IActionResult> ResendCodeUser(int UserId, [FromBody] CodeModel code)
        {
            var result = await user.ResendCodeUser(code, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("SendCodeForgotPassword")]
        public async Task<IActionResult> SendCodeForgotPassword([FromBody] SendCodeForgotPasswordModel code)
        {
            var result = await user.SendCodeForgotPassword(code);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel code)
        {
            var result = await user.ForgotPassword(code);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/ChangeRolUser")]
        public async Task<IActionResult> ChangeRolUser(int UserId)
        {
            var result = await user.ChangeRolUser(UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/UpdateUserImage")]
        public async Task<IActionResult> UpdateUserImage(int UserId, [FromForm] AddImageModel model)
        {
            var result = await user.UpdateUserImage(UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("{UserId}/UpdateUser")]
        public async Task<IActionResult> UpdateUser(int UserId, [FromBody] UpdateUserModel model)
        {
            var result = await user.UpdateUser(UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/GetUser")]
        public async Task<IActionResult> GetUser(int UserId)
        {
            var result = await user.GetUser(UserId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(int UserId)
        {
            var result = await user.DeleteAccount(UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/AddFavorite")]
        public async Task<IActionResult> AddFavorite(int UserId,int CompanyId)
        {
            var result = await user.AddFavorite(UserId, CompanyId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/DeleteFavorite/{FavoriteId}")]
        public async Task<IActionResult> DeleteFavorite(int UserId, int CompanyId,int FavoriteId)
        {
            var result = await user.DeleteFavorite(UserId, CompanyId, FavoriteId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Favorite")]
        public async Task<IActionResult> Favorite(int UserId)
        {
            var result = await user.Favorite(UserId);
            return (result != null && result.Result != null && result.Result.Count > 0) ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpGet("{UserId}/Company/{CompanyId}/Plan/{PlanId}/SuscribePlan")]
        public async Task<IActionResult> SuscribePlan(int UserId, int CompanyId, int PlanId)
        {
            var result = await user.Suscription(UserId, CompanyId, PlanId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }
    }
}
