
using DataAccess.Base;
using DataAccess.Context;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility.Helpers;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);

 
PersianCultureConfig.ConfigurePersianCulture();

// Add services to the container.
builder.Services.AddControllersWithViews();
//Configure Database
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>(); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//Configure Routing
//builder.Services.AddRouting(options => {
//    options.LowercaseUrls = true;
//    options.AppendTrailingSlash = true;

//});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
// Default Admin
    pattern: "{area=Clint}/{controller=Home}/{action=Index}/{id?}/{slug?}/");

app.Run();
