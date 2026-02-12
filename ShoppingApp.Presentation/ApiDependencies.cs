﻿using Microsoft.OpenApi.Models;
using ShoppingApp.Presentation.Constraints;
using ShoppingApp.Presentation.Middlewares;

namespace ShoppingApp.Presentation
{
    public static class ApiDependencies
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails();
            services.AddExceptionHandler<ExceptionHandlingMiddleware>();
            services.AddControllers();

            // Register custom Ulid route constraint
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("ulid", typeof(UlidRouteConstraint));
            });

            services.AddEndpointsApiExplorer();
            
            // Configure Swagger with JWT Bearer authentication
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shopping App API",
                    Version = "v1",
                    Description = "A comprehensive shopping application API built with ASP.NET Core",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohamed Mohyeldein",
                        Email = "madeymohey1@gmail.com"
                    }
                });

                // Add JWT Bearer authentication to Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                                  "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                  "Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
