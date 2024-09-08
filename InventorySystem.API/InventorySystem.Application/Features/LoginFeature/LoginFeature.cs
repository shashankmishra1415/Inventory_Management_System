using Application.Helpers;
using FluentValidation;
using InventorySystem.Application.Features.LoginFeature.Interfaces;
using InventorySystem.Infrastructure.Repositories.Interface;
using InventorySystem.SharedLayer.Models.Request;
using InventorySystem.SharedLayer.Models.Response;
using Microsoft.Extensions.Options;

namespace InventorySystem.Application.Features.LoginFeature
{
    public class LoginFeature : ILoginFeature
    {
        private readonly ILoginRepository loginRepository;
        private readonly IValidator<LoginRequest> validator;
        private readonly IOptions<JWTSettings> jwtSettings;
        public LoginFeature(ILoginRepository loginRepository, IValidator<LoginRequest> validator, IOptions<JWTSettings> jwtSettings)
        {
            this.loginRepository = loginRepository;
            this.validator = validator;
            this.jwtSettings = jwtSettings;
        }

        public async Task<Response> Login(LoginRequest credentials)
        {
            validator.ValidateAndThrow(credentials);
            LoginResponse response = await loginRepository.Login(credentials);
            Response res = new Response();
            if (response != null)
            {
                if (response.IsSuccess == 1)
                {
                    res.Message = "Successfully Login";
                    TokenUtil tokenUtil = new TokenUtil(jwtSettings);
                    JwtTokenResponse jwtToken = new JwtTokenResponse();
                    jwtToken.Token = tokenUtil.GenerateJwtToken(response);
                    res.IsSuccess = response.IsSuccess;
                    res.Result = jwtToken;
                    res.ResponseCode = 200;
                }
                else
                {
                    res.Message = response.Message;
                    res.ResponseCode = 400;
                    res.IsSuccess = 0;
                }
            }
            else
            {
                res.IsSuccess = 0;
                res.Message = "Something went wrong, please try again";
                res.ResponseCode = 500;
            }
            return res;
        }
    }
}
