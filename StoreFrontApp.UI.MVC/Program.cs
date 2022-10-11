using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoreFrontApp.DATA.EF.Models;
using StoreFrontApp.UI.MVC.Data;
//using StoreFrontApp.UI.MVC.Models.delete;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registering StoreFrontApp Database Context
builder.Services.AddDbContext<StoreFrontAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>options.SignIn.RequireConfirmedAccount =true).AddRoles<IdentityRole>().AddRoleManager < RoleManager < IdentityRole >> ().AddEntityFrameworkStores < ApplicationDbContext > ();

builder.Services.AddControllersWithViews();

// Shopping Cart - Step 1
// Register Session - MUST go after .AddControllersWithView();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);//how long the session will be stored in memory
    options.Cookie.HttpOnly = true;//allow us to set cookie options
    options.Cookie.IsEssential = true;//Can't be declined - if someone wants to use our site, must accept this cookie for session
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Shopping Cart - Step 1 Part 2
// Register Session with the app
// This always goes after UseRouting() and before UseAuthentication()
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
