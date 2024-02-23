using Microsoft.AspNetCore.Identity;
using CityBookMVCOnionApplication.ServiceRegistrations;
using CityBookMVCOnionDomain.Entities;
using CityBookMVCOnionInfrastructure.ServiceRegistrations;
using CityBookMVCOnionPersistence.ServiceRegistrations;
using CityBookMVCOnionInfrastructure.Implementations;
using CityBookMVCOnionPersistence.Contexts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = $"/Account/Login/{opt.ReturnUrlParameter}");
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    initializer.InitializeDbContextAsync().Wait();
    initializer.CreateUserRolesAsync().Wait();
    initializer.InitializeAdminAsync().Wait();
}




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
