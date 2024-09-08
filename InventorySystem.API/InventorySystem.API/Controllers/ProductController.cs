using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.ProductFeature;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    [TokenAuthorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductFeature productFeature;

        public ProductController(IProductFeature productFeature)
        {
            this.productFeature = productFeature;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Product([BindRequired] int pageNum, [BindRequired] int pageSize, string? productSku, string? productName, string? eanCode, int categoryId = 0, int manufacturerId = 0)
        {
            try
            {
                Response res = await productFeature.Product(pageNum, pageSize, productSku, productName, eanCode, manufacturerId, categoryId);
                if (res == null)
                {
                    var badRequestResponse = new ApiResponse("Invalid request parameters.", null, Status400BadRequest);
                    badRequestResponse.IsError = true;
                    return BadRequest(badRequestResponse);
                }
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
        public async Task<IActionResult> Product(int id)
        {
            try
            {
                Response res = await productFeature.Product(id);
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

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> Product(ProductRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await productFeature.Product(request, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (ValidationException ex)
            {
                var response = new ApiResponse(ex.Message, await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, 400);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> Product(ProductRequest request, int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await productFeature.Product(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (ValidationException ex)
            {
                var response = new ApiResponse(ex.Message, await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
                response.IsError = true;
                response.StatusCode = 400;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, 500);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await productFeature.Product(id, user.Id);
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

		[HttpGet("totalcount")]
		[ProducesResponseType(typeof(ApiResponse), Status200OK)]
		public async Task<IActionResult> TotalCount()
		{
			try
			{
				Response res = await productFeature.TotalCount();
				var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
				response.IsError = !Convert.ToBoolean(res.IsSuccess);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = new ApiResponse("Something went wrong, Please try again", null, Status500InternalServerError);
				response.IsError = true;
				return StatusCode(500, response);
			}
		}
	}
}
