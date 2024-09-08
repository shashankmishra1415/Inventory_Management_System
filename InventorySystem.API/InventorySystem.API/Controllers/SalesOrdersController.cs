using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.SalesOrdersFeature;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace InventorySystem.API.Controllers
{
    [Route("api/sales-orders")]
    [ApiController]
    [TokenAuthorize]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrdersFeature salesOrdersFeature;
        public SalesOrdersController(ISalesOrdersFeature salesOrdersFeature)
        {
            this.salesOrdersFeature = salesOrdersFeature;
        }

        [HttpGet("item-information")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ItemsInformation([BindRequired] int pageNum, [BindRequired] int pageSize)
        {
            try
            {
                Response res = await salesOrdersFeature.ItemsInformation(pageNum, pageSize);
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

        [HttpPost("basic-information")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> BasicInfo(SalesOrderBasicInformationRequest request)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.BasicInfo(request, user.Id);
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
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("basic-information")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> BasicSalesItems([BindRequired] int pageNum, [BindRequired] int pageSize, string? orderNo, int? customerId, int? movementTypeId, int? warehouseId, int? outType, DateTime? fromDate, DateTime? toDate, int? statusId)
        {
            try
            {
                Response res = await salesOrdersFeature.BasicSalesItems(pageNum, pageSize, orderNo, customerId, movementTypeId, warehouseId, outType, fromDate, toDate, statusId);
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
        public async Task<IActionResult> SalesOrderItem(int id)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.SalesOrderItem(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("item-information/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Item(int id)
        {
            try
            {
                Response res = await salesOrdersFeature.Item(id);
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

        [HttpGet("basic-information/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> BasicInformation(int id)
        {
            try
            {
                Response res = await salesOrdersFeature.BasicInformation(id);
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

        [HttpGet("product-sku")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ProductSKU()
        {
            try
            {
                Response res = await salesOrdersFeature.ProductSKU();
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

        [HttpGet("total-count")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> TotalCount()
        {
            try
            {
                Response res = await salesOrdersFeature.TotalCount();
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

        [HttpPost("items")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> AddMultipleItems(List<SalesOrderItemsInformationRequest> request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = new Response();
                res = await salesOrdersFeature.MultipleItems(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPost("import-sales-outward-excel")]
        [ProducesResponseType(typeof(ApiResponse),Status201Created)]
        public async Task<IActionResult> SalesOutwardExcelData(List<SaleOrderExcelInwardRequest> request, int salesOrderId)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = new Response();
                res = await salesOrdersFeature.SalesOutwardExcelData(request, salesOrderId, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPost("check-duplicate-sales-order-number/{salesOrderNo}")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> CheckDuplicateSalesOrderNumber(string salesOrderNo)
        {
            try
            {
                var res = await salesOrdersFeature.CheckDuplicateSalesOrderNumber(salesOrderNo);
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

        [HttpPut("update-status/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> SaleOrderStatus(SaleOrderStatusRequest request, int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.SaleOrderStatus(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("stock-outward-list/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> SalesOutwardItem(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.SalesOutwardItem(id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("stock-outward")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> StockOutward([BindRequired] int pageNum, [BindRequired] int pageSize)
        {
            try
            {
                Response res = await salesOrdersFeature.StockOutward(pageNum, pageSize);
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

        [HttpGet("direct-stock-outward")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> DirectStockOutward([BindRequired] int pageNum, [BindRequired] int pageSize)
        {
            try
            {
                Response res = await salesOrdersFeature.DirectStockOutward(pageNum, pageSize);
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

        [HttpPatch("mark-dispatch/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> MarkItemAsDispatched(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.MarkStockDispatched(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("mark-complete/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> MarkStockComplete(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.MarkStockComplete(id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("stock-outward-products/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ProductsOfDirectOutward(int id)
        {
            try
            {
                Response res = await salesOrdersFeature.ProductsOfDirectOutward(id);
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

        [HttpPost("direct-outward-scan")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> ScanDirectOutwardMobile(ScanMobileDirectOutwardRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.ScanDirectOutwardMobile(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("direct-outward-scanned")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> SalesDirectOutwardDetails(int soiId, int sobId)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.SalesDirectOutwardDetails(soiId, sobId);
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

        [HttpGet("dispatched-count/{salesOrderId}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> DispatchedCountBySalesOrder(int salesOrderId)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.DispatchedCountBySalesOrder(salesOrderId);
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

        [HttpPost("serial-number-history")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> SerialNumberHistory(SerialNumberHistoryRequest request)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.SerialNumberHistory(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("mark-return/{serialnumber}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> MarkItemAsReturn(string serialnumber)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.MarkItemAsReturn(serialnumber, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("mark-damage/{serialnumber}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> MarkItemAsDamaged(string serialNumber, IsDamgeRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await salesOrdersFeature.MarkItemAsDamaged(serialNumber, user.Id, request);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("scan-all-info")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ScanAllInfo(int id)
        {
            try
            {
                Response res = await salesOrdersFeature.ScanAllInfo(id);
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

        [HttpGet("scanned-unscanned")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ScannedUnscanned(int id)
        {
            try
            {
                Response res = await salesOrdersFeature.ScannedUnscanned(id);
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

        [HttpGet("serial-number-sales-order")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> SerialNumberSalesOrder(int id, string serialNo)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await salesOrdersFeature.SerialNumberSalesOrder(id, serialNo, user.Id);
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

        [HttpPost("direct-serial-number-sales-order")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> DirectSeriaNumberSalesOrder(DirectSerialNoSalesOrder request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await salesOrdersFeature.DirectSeriaNumberSalesOrder(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("mark-complete")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> MarkComplete(int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await salesOrdersFeature.MarkComplete(id, user.Id);
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

        [HttpGet("inventory-to-excel")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ConvertInventoryToExcel(int id)
        {

            try
            {
                byte[] byteArray = await salesOrdersFeature.ConvertInventoryToExcel(id);
                Response res = new Response();
                res.Message = "Excel downloaded successfully.";
                res.Result = File(byteArray, "application/vnd.ms-excel", "saleorderDetail (" + DateTime.Now.ToString("F") + ").xlsx");
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