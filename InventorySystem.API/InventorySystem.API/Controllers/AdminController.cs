using AutoWrapper.Wrappers;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.Admin_Feature.Interfaces;
using InventorySystem.SharedLayer.Models.Response;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
namespace InventorySystem.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [TokenAuthorize]
    public class AdminController : ControllerBase
    {
        readonly IAdminFeature adminFeature;
        public AdminController(IAdminFeature adminFeature)
        {
            this.adminFeature = adminFeature;
        }

        [HttpGet("stock-count-by-warehouse")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockCountByWarehouse()
        {
            try
            {
                Response res = await adminFeature.StockCountByWarehouse();
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

        [HttpGet("received-goods-by-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult>ReceivedGoodsDetailsByLocation(string monthName, int locationId)
        {
            try
            {
                Response res = await adminFeature.ReceivedGoodsDetailsByLocation(monthName, locationId);
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

        [HttpGet("dispatched-goods-by-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> DispatchedGoodsDetailsByLocation(string filterMonth, int locationId)
        {
            try
            {
                Response res = await adminFeature.DispatchedGoodsDetailsByLocation(filterMonth, locationId);
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

        [HttpGet("inventory-detail-by-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> InventoryDetailAtLocation(int locationId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailAtLocation(locationId);
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

        [HttpGet("inventory-by-brand-for-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> InventoryDetailByBrandLocation(int locationId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailByBrandLocation(locationId);
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

        [HttpGet("inventory-by-vendor-for-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> InventoryDetailByVendorForLocation(int locationId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailByVendorForLocation(locationId);
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

        [HttpGet("inventory-by-category-for-location")]
        public async Task<IActionResult> InventoryDetailByCategoryForLocation(int warehouseId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailByCategoryForLocation(warehouseId);
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

        [HttpGet("inventory-for-category-on-location")]
        public async Task<IActionResult> InventoryDetailForCategoryOnLocation(int warehouseId, int categoryId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailForCategoryOnLocation(warehouseId, categoryId);
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

        [HttpGet("search-by-serial-number")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> SearchBySerialNumber(string serialNumber)
        {
            try
            {
                Response res = await adminFeature.SearchBySerialNumber(serialNumber);
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

        [HttpGet("inventory-details-by-brand-for-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> InventoryByBrandLocation(int warehouseId)
        {
            try
            {
                Response res = await adminFeature.InventoryByBrandLocation(warehouseId);
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

        [HttpGet("inventory-details-by-category-for-location")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> InventoryDetailByCategoryLocation(int warehouseId)
        {
            try
            {
                Response res = await adminFeature.InventoryDetailByCategoryLocation(warehouseId);
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
    }
}
