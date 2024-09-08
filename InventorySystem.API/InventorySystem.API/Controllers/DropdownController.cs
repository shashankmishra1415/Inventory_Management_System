using AutoWrapper.Wrappers;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.Dropdown_Feature;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
	[Route("api/dropdown")]
	[ApiController]
	[TokenAuthorize]
	public class DropdownController : ControllerBase
	{
		private readonly IDropdownFeature dropdownFeature;
        private readonly ILogger<DropdownController> logger;
        public DropdownController(IDropdownFeature dropdownFeature, ILogger<DropdownController> logger)
		{
			this.dropdownFeature = dropdownFeature;
            this.logger = logger;
        }

		[HttpGet("vendor-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> VendorType()
		{
			try
			{
				Response res = await dropdownFeature.VendorType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "VendorType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("category")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> Category()
		{
			try
			{
				Response res = await dropdownFeature.Category();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "Category():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("department")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> Department()
		{
			try
			{
				Response res = await dropdownFeature.Department();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "Department():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("manufacturer")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> Manufacturer()
		{
			try
			{
				Response res = await dropdownFeature.Manufacturer();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "Manufacturer():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("status")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> Status()
		{
			try
			{
				Response res = await dropdownFeature.Status();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "Status():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("warehouse-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> WarehouseType()
		{
			try
			{
				Response res = await dropdownFeature.WarehouseType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "WarehouseType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("company-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> CompanyType()
		{
			try
			{
				Response res = await dropdownFeature.CompanyType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "CompanyType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("warehouse-location")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> WarehouseLocation()
		{
			try
			{
				Response res = await dropdownFeature.WarehouseLocation();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "WarehouseLocation():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

        [HttpGet("customer-type")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> CustomerType()
        {
            try
            {
                Response res = await dropdownFeature.CustomerType();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "CustomerType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

        [HttpGet("custom-movement-type")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> CustomMovementType()
        {
            try
            {
                Response res = await dropdownFeature.CustomMovementType();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "CustomMovementType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

        [HttpGet("movement-type")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> MovementType()
        {
            try
            {
                Response res = await dropdownFeature.MovementType();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "MovementType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

        [HttpGet("sale-order-status")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> SaleOrderStatus()
        {
            try
            {
                Response res = await dropdownFeature.SaleOrderStatus();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "SaleOrderStatus():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

        [HttpGet("product-sku")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ProductSKU()
        {
            try
            {
                Response res = await dropdownFeature.ProductSKU();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "ProductSKU():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

		[HttpGet("out-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> OutType()
		{
			try
			{
				Response res = await dropdownFeature.OutType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "OutType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

		[HttpGet("sale-order-movement-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> SaleOrderMovementType()
		{
			try
			{
				Response res = await dropdownFeature.SaleOrderMovementType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "SaleOrderMovementType():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}

        [HttpGet("user")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> User()
        {
            try

            {
                Response res = await dropdownFeature.User();
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.logger.LogError(1, "User():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
        }

		[HttpGet("stock-audit-category-dropdown")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> StockAuditCategoryDropdown()
		{
			try
			{
				Response res = await dropdownFeature.StockAuditCategoryDropdown();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "StockAuditCategoryDropdown():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}
		[HttpGet("action-type-dropdown")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> ActionTypeDropdown()
		{
			try
			{
				Response res = await dropdownFeature.ActionTypeDropdown();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "ActionTypeDropdown():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}
		
		[HttpGet("record-type")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> RecordType()
		{
			try
			{
				Response res = await dropdownFeature.RecordType();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
                this.logger.LogError(1, "Dropdown():" + (ex.Message.Length > 490 ? ex.Message.Substring(0, 490) : ex.Message), ex);
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(500, response);
            }
		}
	}
}
