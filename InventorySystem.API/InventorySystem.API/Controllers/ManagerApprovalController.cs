using AutoWrapper.Wrappers;
using InventorySystem.API.Filters;
using InventorySystem.Application.Features.ManagerApprovalFeature.Interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace InventorySystem.API.Controllers
{
    [Route("api/manager-approval")]
    [ApiController]
    [TokenAuthorize]
    public class ManagerApprovalController : ControllerBase
    {
        public IManagerApprovalFeatures managerapprovalfeatures;
        public ManagerApprovalController(IManagerApprovalFeatures managerApprovalFeatures) 
        {
            this.managerapprovalfeatures=managerApprovalFeatures;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status201Created)]
        public async Task<IActionResult> ManagerApproval(ManagerApprovalRequest request)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await managerapprovalfeatures.ManagerApproval(request,user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? Created("", response) : BadRequest(response);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var response = new ApiResponse(ex.Message, await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
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
        public async Task<IActionResult> ManagerApproval(ManagerApprovalUpdateRequest request, int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await managerapprovalfeatures.ManagerApproval(request, id, user.Id);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return res.IsSuccess == 1 ? NoContent() : BadRequest(response);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var response = new ApiResponse(ex.Message, await ValidationHelper.ValidationFaliure(ex.Errors.ToList()));
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

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> ManagerApproval([BindRequired] int pageNum, [BindRequired] int pageSize, int recordType, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                Response res = await managerapprovalfeatures.ManagerApproval(pageNum, pageSize, recordType,fromDate,toDate);
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
        public async Task<IActionResult> ManagerApproval(int id)
        {
            try
            {
                Response res = await managerapprovalfeatures.ManagerApproval(id);
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
