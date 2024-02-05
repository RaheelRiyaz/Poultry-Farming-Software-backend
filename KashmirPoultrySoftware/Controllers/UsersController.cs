using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KashmirPoultrySoftware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }




        [HttpPost]
        public async Task<APIResponse<UserSignupResponse>> Signup(UserSignup model)
        {
            return await userService.Signup(model);
        }



        [HttpPost("login")]
        public async Task<APIResponse<LoginResponse>> Login(LoginRequest model)
        {
            return await userService.Login(model);
        }



        [Authorize]
        [HttpPost("change-password")]
        public async Task<APIResponse<string>> ChangePassword(ChangePasswordRequest model)
        {
            return await userService.ChangePassword(model);
        }




        [HttpPost("forgot-password")]
        public async Task<APIResponse<string>> ForgotPassword(ForgotPassword model)
        {
            return await userService.ForgotPassword(model.Email);
        }


        [HttpPost("reset-password")]
        public async Task<APIResponse<string>> ResetPassword(ResetPasswordModel model)
        {
            return await userService.ResetPassword(model);
        }



        [Authorize]
        [HttpGet("details")]
        public async Task<APIResponse<UserInfo>> UserInfo()
        {
            return await userService.UserInfo();
        }


        [HttpPut("info")]
        [Authorize]
        public async Task<APIResponse<string>> UpdateUser(UserInfo model)
        {
            return await userService.UpdateUser(model);
        }
    }
}
