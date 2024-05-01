using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class AccountInformationController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public AccountInformationController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogOut() 
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(controllerName:"Login",actionName:"Index");
		}
	}
}
