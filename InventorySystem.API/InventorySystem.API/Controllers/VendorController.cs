using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.VendorFeature.Interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace InventorySystem.API.Controllers
{
    [Route("api/vendor")]
    [ApiController]
    [TokenAuthorize]
    public class VendorController : ControllerBase
    {
        private readonly IVendorFeature vendorFeature;
        private readonly ILogger<VendorController> logger;
        public VendorController(IVendorFeature vendorFeature, ILogger<VendorController> logger)
        {
            this.vendorFeature = vendorFeature;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Vendor([BindRequired] int pageNum, [BindRequired] int pageSize, string? companyName, string? contactName, int typeId = 0, int vendorTypeId = 0, int statusId = 0)
        {
            try
            {
                Response res = await vendorFeature.Vendor(pageNum, pageSize, companyName, contactName, typeId, vendorTypeId, statusId);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(1, "Vendor" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Vendor(VendorRequest request)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await vendorFeature.Vendor(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                if (res.IsSuccess == 1)
                {
                    return Created("", response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> Vendor(VendorRequest request, int id)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await vendorFeature.Vendor(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                if (res.IsSuccess == 1)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Vendor(int id)
        {
            try
            {
                Response res = await vendorFeature.Vendor(id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await vendorFeature.Vendor(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                if (res.IsSuccess == 1)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }

        [HttpGet("vendor-count")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> VendorCount()
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await vendorFeature.VendorCount();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                if (res.IsSuccess == 1)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }
    }
}