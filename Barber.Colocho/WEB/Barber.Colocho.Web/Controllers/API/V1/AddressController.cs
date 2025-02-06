using Asp.Versioning;
using Barber.Colocho.Core.Address;
using Barber.Colocho.Models.Address;
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
    public class AddressController : ControllerBase
    {
        #region Constructor
        private readonly AddressBL addressBL;
        public AddressController(AddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        #endregion

        [HttpPost("{UserId}/AddAddressUser")]
        public async Task<IActionResult> AddAddressUser(int UserId, [FromBody] AddAddressModel model)
        {
            var result = await addressBL.AddAddressUser(UserId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/DeleteAddress/{IdAddress}")]
        public async Task<IActionResult> DeleteAddress(int UserId, int IdAddress)
        {
            var result = await addressBL.DeleteAddress(UserId, IdAddress);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/GetListAddressByUserId")]
        public async Task<IActionResult> GetListAddressByUserId(int UserId)
        {
            var result = await addressBL.GetListAddressByUserId(UserId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/GetAddressById/{AddressId}")]
        public async Task<IActionResult> GetAddressById(int UserId, int AddressId)
        {
            var result = await addressBL.GetAddressById(AddressId, UserId);
            return (result != null && result.Result != null) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{UserId}/UpdateDefaultAddress/{AddressId}")]
        public async Task<IActionResult> UpdateDefaultAddress(int UserId, int AddressId)
        {
            var result = await addressBL.UpdateDefaultAddress(AddressId, UserId);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/UpdateAddress/{AddressId}")]
        public async Task<IActionResult> UpdateAddress(int UserId, int AddressId, [FromBody]AddAddressModel model)
        {
            var result = await addressBL.UpdateAddressUser(UserId, AddressId, model);
            return (result != null && result.Result) ? Ok(result) : BadRequest(result);
        }
    }
}
