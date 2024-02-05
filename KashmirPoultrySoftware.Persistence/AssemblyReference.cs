using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Persistence.Data;
using KashmirPoultrySoftware.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Persistence
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRpository>();
            services.AddScoped<IHatchRepository, HatchRepository>();
            services.AddScoped<IMotivalityRepository, MotivalityRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddDbContextPool<KashmirPoultrySoftwareDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(KashmirPoultrySoftwareDbContext)));
            });

            return services;
        }
    }
}
