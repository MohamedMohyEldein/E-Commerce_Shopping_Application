using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Application.Common.Behaviors;
using ShoppingApp.Application.Common.Services;
using ShoppingApp.Application.Common.Settings;
using ShoppingApp.Application.Features.CartItems.Commands;

namespace ShoppingApp.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings!.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.Key!))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("AppUser"));
                options.AddPolicy("SellerOnly", policy => policy.RequireRole("Seller"));
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(ApplicationDependencies).Assembly, includeInternalTypes: true);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateCartItemCommand).Assembly));
            services.AddAutoMapper(cfg => { }, typeof(ApplicationDependencies).Assembly);

            return services;
        }
    }
}
