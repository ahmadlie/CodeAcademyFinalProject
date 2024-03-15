using DataAccessLayer;
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
			var roles = new[] { "Admin", "Member" };
			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new AppRole() { Name = role });
			}

			var roleAdmin = await roleManager.FindByNameAsync("Admin");
			if (!await userManager.CheckByRole(roleAdmin!, appDbContext))
			{
				AppUser user = new()
				{
					UserName = "admin123",
					Email = "admin.hyper@gmail.com",
					PhoneNumber = "123789",
				};
				var result = await userManager.CreateAsync(user, "admin123");
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, roleAdmin!.Name!);
				}
			}
		}

		private async static Task<bool> CheckByRole(this UserManager<AppUser> userManager, AppRole role, AppDbContext appDbContext)
		{
			
			var adminUser = await userManager.GetUsersInRoleAsync("Admin");			
			if (adminUser.Any()) { return true; }
			return false;
		}
	}
}
