using AutoWrapper.Wrappers;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.StockAuditFeature.interfaces;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
    [Route("api/stock-audit/")]
    [ApiController]
    [TokenAuthorize]
    public class StockAuditController : ControllerBase
    {
        public readonly IStockAuditFeature stockAuditFeature;
        public StockAuditController(IStockAuditFeature StockAuditFeature)
        {
            this.stockAuditFeature = StockAuditFeature;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAudit(StockAuditRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await stockAuditFeature.StockAudit(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, 400);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("warehouse/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAuditByWarehouseId(int id)
        {
            try
            {
                Response res = await stockAuditFeature.StockAuditByWarehouseId(id);
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

        [HttpPut("mark-audit/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> MarkAuditComplete(MarkAuditCompleteRequest request, int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await stockAuditFeature.MarkAuditComplete(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, 500);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPut("mark-audit")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> MarkAuditComplete(int auditId, int categoryId, string serialNumber)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await stockAuditFeature.MarkAuditComplete(auditId, categoryId, serialNumber, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, 500);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAudit([BindRequired] int pageNum, [BindRequired] int pageSize, int? warehouseId, DateTime? fromDate, DateTime? toDate, int? userId, int? status)
        {
            try
            {
                Response res = await stockAuditFeature.StockAudit(pageNum, pageSize, warehouseId, fromDate, toDate, userId, status);
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

        [HttpGet("manufacturer")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAudit(int id, [BindRequired] int pageNum, [BindRequired] int pageSize, string? productSKU, int? manufacturerName, int? categoryName)
        {
            try
            {
                Response res = await stockAuditFeature.StockAudit(id, pageNum, pageSize, productSKU, manufacturerName, categoryName);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAudit(int id)
        {
            try
            {
                Response res = await stockAuditFeature.StockAudit(id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return BadRequest(response);
            }
        }

        [HttpGet("location/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockAuditLocation(int id)
        {
            try
            {
                Response res = await stockAuditFeature.StockAuditLocation(id);
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

        [HttpGet("count")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> TotalCount()
        {
            try
            {
                Response res = await stockAuditFeature.TotalCount();
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
