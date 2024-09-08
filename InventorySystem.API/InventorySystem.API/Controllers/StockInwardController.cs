using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.StockInwardFeature.Interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
    [Route("api/stock-inward")]
    [ApiController]
    [TokenAuthorize]
    public class StockInwardController : ControllerBase
    {
        private readonly IStockInwardFeature stockInwardFeature;

        public StockInwardController(IStockInwardFeature stockInwardFeature)
        {
            this.stockInwardFeature = stockInwardFeature;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Invoice(AddInvoiceRequest request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                var res = await stockInwardFeature.Invoice(request, user.Id);
                var response = new ApiResponse(res.Message, res.Id, res.ResponseCode);
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
                var resposne = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()), Status400BadRequest);
                resposne.IsError = true;
                return BadRequest(resposne);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Invoice([BindRequired] int pageNum, [BindRequired] int pageSize, DateTime? fromDate, DateTime? toDate, string? invoiceNo, int vendorCompanyNameId = 0, int moveTypeID = 0, int warehouseLocationId = 0)
        {
            try
            {
                Response res = await stockInwardFeature.Invoice(pageNum, pageSize, fromDate, toDate, invoiceNo, vendorCompanyNameId, moveTypeID, warehouseLocationId);
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Invoice(int id)
        {
            try
            {
                Response res = await stockInwardFeature.Invoice(id);
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

        [HttpPost("receive-inventory")]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> AddProductInInvoice(List<AddProductInInvoiceRequest> request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                var res = await stockInwardFeature.AddProductInInvoice(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return (bool)response.IsError ? BadRequest(response) : Created("",response);
            }
            catch (ValidationException ex)
            {
                var resposne = new ApiResponse("Validation Error", await ValidationHelper.ValidationFaliure(ex.Errors.ToList()), Status400BadRequest);
                resposne.IsError = true;
                return BadRequest(resposne);
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
        public async Task<IActionResult> GetAllProductsOfAnInvoice(int id)
        {
            try
            {
                Response res = await stockInwardFeature.GetAllProductsOfAnInvoice(id);
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

        [HttpGet("add-items/{productSku}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> GetProductDetailForAutoFill(int productSku)
        {
            try
            {
                Response res = await stockInwardFeature.GetProductDetailsForAutoFill(productSku);
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

        [HttpGet("scan-barcode/{productSkuProfileId}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> GetInfoForBarcodePage(int productSkuProfileId)
        {
            try
            {
                Response res = await stockInwardFeature.GetInfoForBarcodePage(productSkuProfileId);
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

        [HttpGet("generated-barcodes/{productSkuProfileId}")]
        public async Task<IActionResult> GetUnscannedSerialNum(int productSkuProfileId)
        {
            try
            {
                Response res = await stockInwardFeature.GetUnscannedSerialNum(productSkuProfileId);
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

        [HttpPost("import-excel")]
        public async Task<IActionResult> StockInwardExcelData(List<InwardExcelRequest> request, int invoiceId)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response result = await stockInwardFeature.InwardExcelData(request, invoiceId, user.Id);
                var response = new ApiResponse("Excel Uploaded Successfully", result.Result, result.ResponseCode);
                if (response != null)
                {
                    response.IsError = false;
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

        [HttpGet("generate-barcodes/{productSkuProfileId}")]
        public async Task<IActionResult> GenerateBarcodes(int productSkuProfileId)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                var result = await stockInwardFeature.GenerateBarcode(productSkuProfileId, user.Id);
                var response = new ApiResponse(result.Message, result.Result, result.ResponseCode);
                response.IsError = !Convert.ToBoolean(result.IsSuccess);
                if (response != null)
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
                return BadRequest(response);
            }
        }

        [HttpGet("serial-number")]
        public async Task<IActionResult> SerialNumber(int productSkuId)
        {
            try
            {
                var result = await stockInwardFeature.SerialNumbers(productSkuId);
                var response = new ApiResponse(result.Message, result.Result, result.ResponseCode);
                if (response != null)
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
                return BadRequest(response);
            }
        }

        [HttpPut("update-serial-number")]
        public async Task<IActionResult> UpdateSerialNumber(string serialNumber)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response result = new Response();
                result = await stockInwardFeature.UpdateSerialNumber(serialNumber, user.Id);
                var response = new ApiResponse(result.Message, result.Result, result.ResponseCode);
                if (response != null)
                {
                    response.IsError = false;
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
                return BadRequest(response);
            }
        }

        [HttpGet("pending-invoice")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> PendingInvoice()
        {
            try
            {
                Response result = await stockInwardFeature.PendingInvoice();
                var response = new ApiResponse(result.Message, result.Result, result.ResponseCode);
                if (response != null)
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
                return BadRequest(response);
            }
        }

        [HttpGet("pending-invoice-scan")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> PendingSerialNumberForScanning(int invoiceNumberId)
        {
            try
            {
                Response res = await stockInwardFeature.PendingSerialNumberForScanning(invoiceNumberId);
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
        [HttpGet("product/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ProductByIdStockInward(int id)
        {
            try
            {
                Response res = await stockInwardFeature.ProductByIdStockInward(id);
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

        [HttpGet("check-duplicate-invoice-number/{invoiceNo}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> CheckDuplicateInvoiceNumber(string invoiceNo)
        {
            try
            {
                Response res = await stockInwardFeature.CheckDuplicateInvoiceNumber(invoiceNo);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("mark-complete/{invoiceId}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> MarkComplete(int invoiceId)
        {
            try
            {
                Response res = await stockInwardFeature.MarkComplete(invoiceId);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = Convert.ToBoolean(res.IsSuccess);
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("update-print-count/{productSkuProfileId}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> UpdatePrintCount(int productSkuProfileId)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await stockInwardFeature.UpdatePrintCount(productSkuProfileId, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = Convert.ToBoolean(res.IsSuccess);
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("scan-all-info")]
        public async Task<IActionResult> ScanAllInfo(int id)
        {
            try
            {
                Response res = await stockInwardFeature.ScanAllInfo(id);
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

        [HttpGet("scan-all-serial-number")]
        public async Task<IActionResult> ScanAllSerialNum(int id)
        {
            try
            {
                Response res = await stockInwardFeature.ScanAllSerialNum(id);
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

        [HttpGet("unscanned-serial-number-by-invoice-id")]
        public async Task<IActionResult> UnscannedSerialNumberByInvoiceId(int id)
        {
            try
            {
                Response res = await stockInwardFeature.UnscannedSerialNumberByInvoiceId(id);
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

        [HttpGet("print-all-by-invoice")]
        public async Task<IActionResult> PrintAllByInvoiceBtnClicked(int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await stockInwardFeature.PrintAllByInvoiceBtnClicked(id, user.Id);
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