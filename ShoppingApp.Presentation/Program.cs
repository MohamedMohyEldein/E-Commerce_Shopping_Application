using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShoppingApp.Application;
using ShoppingApp.Domain.Identities;
using ShoppingApp.Infrastructure;
using ShoppingApp.Infrastructure.Persistence;
using ShoppingApp.Infrastructure.Services;
using ShoppingApp.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddApiDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies(builder.Configuration);

var app = builder.Build();


app.UseExceptionHandler();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping App API v1");
        options.RoutePrefix = "swagger"; // Access Swagger UI at /swagger
        options.DocumentTitle = "Shopping App API Documentation";
        options.DisplayRequestDuration();
    });
}

// Seed roles and super admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        var db = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Seed roles and super admin
        await SeedData.SeedRolesAndSuperAdminAsync(userManager, roleManager, logger);
        
        // Seed cart and wishlist for super admin
        await SeedData.SeedCartAndWishlistForSuperAdminAsync(db, userManager, logger);
        
        logger.LogInformation("Database seeding completed successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}

app.UseStaticFiles();
app.UseHsts();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

