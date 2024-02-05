using AutoMapper;
using KashmirPoultrySoftware.Application.Abstraction.IEmail;
using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.Abstraction.ITokenService;
using KashmirPoultrySoftware.Application.ApiResponse;
using KashmirPoultrySoftware.Application.EmailSetting;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Application.Utilis;
using KashmirPoultrySoftware.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly IHttpContext httpContext;
        private readonly IEmailTemplateRenderer emailTemplateRenderer;
        private readonly IEmailService emailService;

        public UserService(IMapper mapper, IUserRepository userRepository ,ITokenService tokenService, IHttpContext httpContext, IEmailTemplateRenderer emailTemplateRenderer, IEmailService emailService)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.tokenService = tokenService;
            this.httpContext = httpContext;
            this.emailTemplateRenderer = emailTemplateRenderer;
            this.emailService = emailService;
        }




        public async Task<APIResponse<string>> ChangePassword(ChangePasswordRequest model)
        {
            var id = await httpContext.GetId();
            var user = await userRepository.FirstOrDefaultAsync(_=>_.Id == id);
            if (user is null) return APIResponse<string>.ErrorResponse();

            if (!AppEncryption.ComparePassword(user.Password, user.Salt, model.OldPassword)) return APIResponse<string>.ErrorResponse();

            if(AppEncryption.ComparePassword(user.Password,user.Salt, model.NewPassword)) return APIResponse<string>.ErrorResponse("Old Password and New Password cannot be same");

            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, model.NewPassword);

            var res = await userRepository.UpdateAsync(user);

            if (res > 0) return APIResponse<string>.SuccessResponse("Password changed Successfully");

            return APIResponse<string>.ErrorResponse();
        }




        public async Task<APIResponse<string>> ForgotPassword(string email)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.Email == email);
            if (user is null) return APIResponse<string>.ErrorResponse("Email is invalid");

            user.ResetCode = AppEncryption.GenerateNumericResetCode();
            user.ResetCodeExpirationTime = DateTime.Now.AddHours(1);

            var res = await userRepository.UpdateAsync(user);

            if (res <= 0) 
            return APIResponse<string>.ErrorResponse();

            var mail = new MailSetting()
            {
                To = new List<string>() { user.Email },
                Subject = "Reset Password",
                Body = await emailTemplateRenderer.RenderTemplateAsync("ResetCode.cshtml", new { UserName = user.UserName, ResetCode = user.ResetCode })
            };

            var emailSendResponse = await emailService.SendEmailAsync(mail);
            if (emailSendResponse) return APIResponse<string>.SuccessResponse($"Reset Code has been sent to {email}");

            return APIResponse<string>.ErrorResponse();
        }




        public async Task<APIResponse<LoginResponse>> Login(LoginRequest model)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.UserName == model.UserName || _.Email == model.UserName);
            if (user is null) return APIResponse<LoginResponse>.ErrorResponse("Invalid Crdentials");

            if(!AppEncryption.ComparePassword(user.Password, user.Salt, model.Password))
                return APIResponse<LoginResponse>.ErrorResponse("Invalid Crdentials");

            var loginResponse = new LoginResponse(user.UserName, tokenService.GenerateToken(user));

            return APIResponse<LoginResponse>.SuccessResponse(result:loginResponse);
        }




        public async Task<APIResponse<string>> ResetPassword(ResetPasswordModel model)
        {
            var user = await userRepository.FirstOrDefaultAsync(_ => _.UserName == model.UserName || _.Email == model.UserName);

            if (user is null) return APIResponse<string>.ErrorResponse("Invalid Crdentials");

            if (user.ResetCode != model.ResetCode) return APIResponse<string>.ErrorResponse("Reset code is invalid");

            if (user.ResetCodeExpirationTime < DateTime.Now) return APIResponse<string>.ErrorResponse("Reset code has been expired");

            user.Salt = AppEncryption.GenerateSalt();
            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, model.NewPassword);
            user.ResetCode = string.Empty;

            var res = await userRepository.UpdateAsync(user);

            if (res > 0) return APIResponse<string>.SuccessResponse("Password Reset Successfully");

            return APIResponse<string>.ErrorResponse();
        }



        public async Task<APIResponse<UserSignupResponse>> Signup(UserSignup model)
        {
            var userNameExists = await userRepository.IsExistsAsync(_ => _.UserName == model.UserName);
            var contactNoExists = await userRepository.IsExistsAsync(_ => _.ContactNo == model.ContactNo);

            if (userNameExists) return APIResponse<UserSignupResponse>.ErrorResponse("Username is already taken");
            if (contactNoExists) return APIResponse<UserSignupResponse>.ErrorResponse("Contact number is already taken");

            var user = mapper.Map<User>(model);
            user.Salt = AppEncryption.GenerateSalt();
            user.Password = AppEncryption.GenerateHashedPassword(user.Salt, user.Password);

            var res = await userRepository.AddAsync(user);

            if(res > 0) return APIResponse<UserSignupResponse>.SuccessResponse(message:"Signed up successfully",result:mapper.Map<UserSignupResponse>(user));

            return APIResponse<UserSignupResponse>.ErrorResponse();
        }




        public async Task<APIResponse<string>> UpdateUser(UserInfo model)
        {
            var user = await userRepository.GetByIdAsync(await httpContext.GetId());

            if (user is null) return APIResponse<string>.ErrorResponse("No User Found");

            user.ContactNo = model.ContactNo;
            user.Email = model.Email;

            var res = await userRepository.UpdateAsync(user);

            if (res > 0) return APIResponse<string>.SuccessResponse("Details updated successfully");

            return APIResponse<string>.ErrorResponse("Internal Server error");
        }




        public async Task<APIResponse<UserInfo>> UserInfo()
        {
            var user = await userRepository.UserInfo(await httpContext.GetId());

            if (user is null) return APIResponse<UserInfo>.ErrorResponse();

            return APIResponse<UserInfo>.SuccessResponse(result: user);
        }
    }
}
