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
            services.AddSwaggerGen();

            return services;
        }
    }
}
