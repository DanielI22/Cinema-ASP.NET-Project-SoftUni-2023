using CinemaSystem.Data.Models;
using CinemaSystem.Services.Data.Interfaces;
using CinemaSystem.Web.Data;
using CinemaSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static CinemaSystem.Common.GeneralApplicationConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CinemaSystemDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CinemaSystemDbContext>();

builder.Services.AddApplicationServices(typeof(ICinemaService));

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.SeedAdministrator(DevelopmentAdminEmail);
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
app.MapRazorPages();

app.Run();
