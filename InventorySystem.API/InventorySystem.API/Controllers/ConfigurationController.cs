using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.ConfigurationFeature.Interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
    [Route("api/configuration")]
    [ApiController]
    [TokenAuthorize]
    public class ConfigurationController : ControllerBase
    {

        private readonly IConfigurationFeature configurationfeature;
        public ConfigurationController(IConfigurationFeature configurationFeature)
        {
            this.configurationfeature = configurationFeature;
        }

        [HttpGet("category")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Category()
        {
            try
            {
                Response res = await configurationfeature.Category();
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

        [HttpGet("category/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Category(int id)
        {
            try
            {
                Response res = await configurationfeature.Category(id);
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

        [HttpPost("category")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Category(CategoryTypeRequest request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Category(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
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
                return StatusCode(Status500InternalServerError);
            }
        }

        [HttpPut("category/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> Category(CategoryTypeRequest request, int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Category(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
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
                return StatusCode(Status500InternalServerError);
            }
        }

        [HttpDelete("category/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Category(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError);
            }
        }

        [HttpGet("manufacturer")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Manufacturer()
        {
            try
            {
                Response res = await configurationfeature.Manufacturer();
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

        [HttpGet("manufacturer/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Manufacturer(int id)
        {
            try
            {
                Response res = await configurationfeature.Manufacturer(id);
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

        [HttpPost("manufacturer")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Manufacturer(ManufacturerTypeRequest request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Manufacturer(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
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
                return StatusCode(Status500InternalServerError);
            }
        }

        [HttpPut("manufacturer/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> Manufacturer(ManufacturerTypeRequest request, int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Manufacturer(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
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
                return StatusCode(Status500InternalServerError);
            }
        }

        [HttpDelete("manufacturer/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await configurationfeature.Manufacturer(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError);
            }
        }
    }
}


