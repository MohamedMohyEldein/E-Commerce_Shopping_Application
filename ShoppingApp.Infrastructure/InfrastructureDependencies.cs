using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure.Persistence;
using ShoppingApp.Infrastructure.Repositories;

namespace ShoppingApp.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });

            services.AddIdentity<AppUser, AppRole>(options =>
             {
                 options.Password.RequireNonAlphanumeric = true;
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequiredUniqueChars = 3;
                 options.Password.RequiredLength = 6;
             })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFileService>(sp =>
            {
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                return new FileService(env);
            });

            return services;
        }
    }
}
