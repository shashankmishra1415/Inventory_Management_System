using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using InventorySystem.SharedLayer.Models.Request;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InventorySystem.Application.Features.WarehouseFeature.Interfaces;
using InventorySystem.Application.Helpers;
using FluentValidation;
using InventorySystem.API.Filters;
namespace InventorySystem.API.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    [TokenAuthorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseFeature warehouseFeature;
        private readonly ILogger<WarehouseController> logger;
        public WarehouseController(IWarehouseFeature warehouseFeature, ILogger<WarehouseController> logger)
        {
            this.warehouseFeature = warehouseFeature;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Warehouse([BindRequired] int pageNum, [BindRequired] int pageSize, string? locationName, double capacity = 0, int typeId = 0, int statusId = 0)
        {
            try
            {
                Response res = await warehouseFeature.Warehouse(pageNum, pageSize, locationName, capacity, typeId, statusId);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //this.logger.LogInformation(2,"Test log from {p}", "weatherforecast");
                this.logger.LogError(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Warehouse(WarehouseRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await warehouseFeature.Warehouse(request, user.Id);
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
                this.logger.LogWarning(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> Warehouse(WarehouseRequest request, int id)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await warehouseFeature.Warehouse(request, id, user.Id);
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
                this.logger.LogWarning(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Warehouse(int id)
        {

            try
            {
                Response res = await warehouseFeature.Warehouse(id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(1, "Warehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await warehouseFeature.DeleteWarehouse(id,user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(1, "DeleteWarehouse():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError,response);
            }
        }


		[HttpGet("count")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> Count()
		{
			try
			{
				Response res = await warehouseFeature.Count();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
				this.logger.LogWarning(1, "Count():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
				var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
				response.IsError = true;
				return StatusCode(Status500InternalServerError, response);
			}
		}

	}
}