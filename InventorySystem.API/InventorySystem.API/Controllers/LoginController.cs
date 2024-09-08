using AutoWrapper.Wrappers;
using FluentValidation;
using InventorySystem.Application.Features.LoginFeature.Interfaces;
using InventorySystem.Application.Helpers;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace InventorySystem.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginFeature loginFeature;

        public LoginController(ILoginFeature loginFeature)
        {
            this.loginFeature = loginFeature;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<IActionResult> Login(LoginRequest credentials)
        {
            try
            {
                Response res = await loginFeature.Login(credentials);
                var response = new ApiResponse(res.Message, res.Result, res.ResponseCode);
                response.IsError = !Convert.ToBoolean(res.IsSuccess);
                return Ok(response);
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
                return StatusCode(500, response);
            }
        }
    }
}
