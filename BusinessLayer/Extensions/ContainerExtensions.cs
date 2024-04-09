using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Helper;
using DataAccessLayer;
using DataAccessLayer.Repository.Abtract;
using DataAccessLayer.Repository.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
	public static class ContainerExtensions
	{
		public async static Task CheckAndCreateRole(this IServiceScope serviceScope)
		{
			var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
			var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
			var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
			var roles = new[] { "SuperAdmin", "Admin", "Member" };
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new AppRole() { Name = role });
			}

			var roleSuperAdmin = await roleManager.FindByNameAsync("SuperAdmin");
			if (!await userManager.CheckByRole(roleSuperAdmin!, appDbContext))
			{
				AppUser user = new()
				{
					UserName = "superadmin123",
					Email = "superadmin.hyper@gmail.com",
					PhoneNumber = "123789",
				};
				var result = await userManager.CreateAsync(user, "superadmin123");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, roleSuperAdmin!.Name!);
				}
			}
		}

		private async static Task<bool> CheckByRole(this UserManager<AppUser> userManager, AppRole role, AppDbContext appDbContext)
		{

			var adminUser = await userManager.GetUsersInRoleAsync("SuperAdmin");
			if (adminUser.Any()) { return true; }
			return false;
		}

		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IPostRepository, PostRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IImageRepository, ImageRepository>();
			services.AddScoped<IPostService, PostService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddSession();
			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = "/Errors/AccessDenied";
				options.LoginPath = "/Login/Index";
			});

			return services;
		}
	}
}
