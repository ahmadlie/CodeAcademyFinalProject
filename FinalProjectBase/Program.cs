using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Helper;
using DataAccessLayer;
using DataAccessLayer.Repository.Abtract;
using DataAccessLayer.Repository.Abtract.Base;
using DataAccessLayer.Repository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using FinalProjectBase.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLayer.Extensions;
using FinalProjectBase.Middlewares.Token;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Org.BouncyCastle.Math.EC.ECCurve;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
	opt.Password.RequireNonAlphanumeric = false;
	opt.User.RequireUniqueEmail = false;
	opt.Password.RequireUppercase = false;
	opt.Password.RequireLowercase = false;
	opt.Password.RequiredLength = 1;
})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddSignInManager<SignInManager<AppUser>>()
	.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var mapperConfiguration = new MapperConfiguration(cfg =>
{
	cfg.AddProfile(new MapperProfile());
	cfg.AddProfile(new UIMapperProfile());
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());


builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.Name = "AspNetCore_Auth";
	options.LoginPath = "/Login/Index";
	options.AccessDeniedPath = "/Errors/AccessDenied";
	options.Cookie.Expiration = TimeSpan.FromDays(1);
});

// Extension For Dependencies
builder.Services.AddServiceDependencies();
var app = builder.Build();


// Middlewares Area
// Middlewares Area
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

using (var scope = app.Services.CreateScope())
{
	await scope.CheckAndCreateRole();
}

app.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
