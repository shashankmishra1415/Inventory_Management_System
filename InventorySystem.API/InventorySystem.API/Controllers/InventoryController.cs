using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.SharedLayer.Models.Response;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.InventoryFeatures.interfaces;
using InventorySystem.SharedLayer.Models.Request;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryFeature inventoryFeature;
        public InventoryController(IInventoryFeature inventoryFeature)
        {
            this.inventoryFeature = inventoryFeature;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Inventory([BindRequired] int pageNum, [BindRequired] int pageSize, [BindRequired] DateTime startDate, [BindRequired] DateTime endDate, string? productSKU, string? sortColumn, string? sortOrder, int warehouseId)
        {
            try
            {
				UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                warehouseId = warehouseId == 0 ? user.Warehouse : warehouseId;

                Response res = await inventoryFeature.Inventory(pageNum, pageSize, startDate, endDate, productSKU, sortColumn, sortOrder, warehouseId);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("ConvertInvetoryToExcel")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ConvertInventoryToExcel([BindRequired] DateTime startDate, [BindRequired] DateTime endDate, string? name, int locationId)
        {

            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                locationId = locationId == 0 ? user.Warehouse : locationId;
                byte[] byteArray = await inventoryFeature.ConvertInventoryToExcel(startDate, endDate, name, locationId);
                Response res = new Response();
                res.Message = "Excel downloaded successfully.";
                res.Result = File(byteArray, "application/vnd.ms-excel", "InventoryReport (" + DateTime.Now.ToString("F") + ").xlsx");
                res.ResponseCode = 200;
                res.IsSuccess = 1;
                return Ok(res);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
