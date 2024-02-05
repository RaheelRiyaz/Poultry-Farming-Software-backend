using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.Utilis;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.ContextService
{
    internal class HttpContextService : IHttpContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> GetId()
        {
            var id = Guid.Empty;
            await Task.Run(() =>
            {
               var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == AppClaim.Id)?.Value;
               id = userId is not null ? Guid.Parse(userId) : Guid.Empty;
            });

            return id;
        }

        public async Task<string> GetUserName()
        {
            return await Task.Run(() =>
            {
                var username = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == AppClaim.UserName)?.Value;
                return username ?? string.Empty;
            });
        }
    }
}
