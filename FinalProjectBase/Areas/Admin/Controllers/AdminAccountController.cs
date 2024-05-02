using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class AdminAccountController : Controller
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly UserManager<AppUser> userManager;

		public AdminAccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> LogOutAdmin()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(actionName: "LoginAdmin");
		}

		[HttpGet]
		public async Task<IActionResult> LoginAdmin()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var user = await userManager.FindByNameAsync(username);
			if (user is null)
			{
				TempData["Message"] = "Not Found";
				return RedirectToAction(nameof(LoginAdmin));
			}
			else
			{
				var res = await signInManager.CheckPasswordSignInAsync(user, password, false);
				if (!res.Succeeded) 
				{
					TempData["Message"] = "Wrong!";
					return RedirectToAction(nameof(LoginAdmin));
				}
				else
				{
					return RedirectToAction(controllerName: "AdminUser", actionName: "Index");
				}
			}

		}


	}
}
