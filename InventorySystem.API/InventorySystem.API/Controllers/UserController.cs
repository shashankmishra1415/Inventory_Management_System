using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using InventorySystem.Application.Features.UserFeature.Interfaces;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.Application.Helpers;
using FluentValidation;
using InventorySystem.API.Filters;

namespace InventorySystem.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [TokenAuthorize]
    public class UserController : ControllerBase
    {
        private readonly IUserFeature userFeature;
        public UserController(IUserFeature userFeature)
        {
            this.userFeature = userFeature;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> User(int id)
        {
            try
            {
                Response res = await userFeature.User(id);
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

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> User([BindRequired] int pageNum, [BindRequired] int pageSize, string? name, string? mobile, int status, int warehouseId = 0, int departmentId = 0)
        {

            try
            {
                Response res = await userFeature.User(pageNum, pageSize, name, mobile, status, warehouseId, departmentId);
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
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
                Response res = await userFeature.DeleteUser(id, user.Id);
                return NoContent();
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
        public async Task<IActionResult> User(SaveUserRequest request)
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await userFeature.User(request, user.Id);
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> User(EditUserRequest request, int id)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await userFeature.User(request, id, user.Id);
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
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPut("change-credentials/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> UpdateLoginUser(UpdateUserLogin request, int id)
        {

            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await userFeature.UpdateLoginUser(request, id, user.Id);
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
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("details-for-mobile")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> UserDetailForMobile()
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await userFeature.UserDetailForMobile(user.Id);
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

        [HttpGet("profile-detail")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> UserProfileDetail()
        {
            UserRequest user = (UserRequest)HttpContext.Items["UserConfig"];
            try
            {
                Response res = await userFeature.UserProfileDetail(user.Id);
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

        [HttpPut("profile/{id}")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> EditUserProfile(EditUserProfileRequest request, int id)
        {
            try
            {
                Response res = await userFeature.EditUserProfile(request, id);
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
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpPatch("reset-password")]
        [ProducesResponseType(typeof(ApiResponse), Status204NoContent)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request, int id)
        {
            try
            {
                Response res = await userFeature.ResetPassword(request, id);
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
            catch (Exception ex)
            {
                var response = new ApiResponse(ex.Message, null, Status500InternalServerError);
                response.IsError = true;
                return StatusCode(Status500InternalServerError, response);
            }
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Count()
        {
            try
            {
                Response res = await userFeature.Count();
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
