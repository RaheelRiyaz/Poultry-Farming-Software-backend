using KashmirPoultrySoftware.Application.Abstraction.IEmail;
using KashmirPoultrySoftware.Application.Abstraction.ITokenService;
using KashmirPoultrySoftware.Infrastructure.MailSetting;
using KashmirPoultrySoftware.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfraStructureServices(this  IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailOption>(configuration.GetSection("MailJetOptionSection"));
            services.AddSingleton<ITokenService>(new TokenService(configuration));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();
            return services;
        }
    }
}
