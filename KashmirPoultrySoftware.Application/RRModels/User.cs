using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record UserSignup(string UserName, string Email, string ContactNo, string Password);

    public record UserSignupResponse(Guid Id, string UserName, string Email, string ContactNo);

    public record LoginRequest(string UserName, string Password);

    public record LoginResponse(string UserName, string Token);

    public record ChangePasswordRequest(string OldPassword, string NewPassword);
    public record ForgotPassword(string Email);
    public record ResetPasswordModel
    (
         string UserName,
         string ResetCode,
         string NewPassword
    );


    public class UserInfo
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
    }


}
