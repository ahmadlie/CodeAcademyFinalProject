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
	.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var mapperConfiguration = new MapperConfiguration(cfg =>
{
	cfg.AddProfile(new MapperProfile());
	cfg.AddProfile(new UIMapperProfile());
});
builder.Services.AddSingleton(mapperConfiguration.CreateMapper());
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,

						ValidAudience = builder.Configuration["Token:Audience"],
						ValidIssuer = builder.Configuration["Token:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey
						(
							Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!
						))
					};
				});



var app = builder.Build();
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


using (var scope = app.Services.CreateScope())
{
	await scope.CheckAndCreateRole();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();