using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IServices
{
    public interface IUserService 
    {
        Task<APIResponse<UserSignupResponse>> Signup(UserSignup model);
        Task<APIResponse<LoginResponse>> Login(LoginRequest model);
        Task<APIResponse<string>> ChangePassword(ChangePasswordRequest model);
        Task<APIResponse<string>> ForgotPassword(string email);
        Task<APIResponse<string>> ResetPassword(ResetPasswordModel model);
        Task<APIResponse<UserInfo>> UserInfo();
        Task<APIResponse<string>> UpdateUser(UserInfo model);
    }
}
