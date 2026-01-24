using ShoppingApp.Application;
using ShoppingApp.Infrastructure;
using ShoppingApp.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddApiDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies(builder.Configuration);

var app = builder.Build();


app.UseExceptionHandler();


if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var db = services.GetRequiredService<ApplicationDbContext>();

//    try
//    {
//        await SeedData.SeedAsync(db);
//    }
//    catch (Exception ex)
//    {
//        // log error
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}

app.UseStaticFiles();
app.UseHsts();
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

