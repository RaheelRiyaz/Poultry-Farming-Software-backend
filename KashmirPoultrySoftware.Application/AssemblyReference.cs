using DinkToPdf.Contracts;
using DinkToPdf;
using KashmirPoultrySoftware.Application.Abstraction.IServices;
using KashmirPoultrySoftware.Application.ContextService;
using KashmirPoultrySoftware.Application.Services;
using KashmirPoultrySoftware.Application.Utilis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,string rootPath)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHatchService, HatchService>();
            services.AddScoped<IMotivalityService, MotivalityService>();
            services.AddScoped<IExpenditureService, ExpenditureService>();
            services.AddScoped<IHttpContext,HttpContextService>();
            services.AddSingleton<IStorageService>(new StoragService(rootPath));
            services.AddScoped<IFileService,FileService>();
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<ISaleService,SaleService>();
            services.AddScoped<HatchValidator,HatchValidator>();
            services.AddScoped<IPdfService,PdfService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            return services;
        }
    }
}
