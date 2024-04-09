using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectBase.Controllers
{
	[Authorize(Roles = "Member")]
	public class AuthorProfileController : Controller
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		public AuthorProfileController(IUserService userService, UserManager<AppUser> userManager)
		{
			_userService = userService;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var appUserDTO = await _userService.GetCurrentUserAsync(HttpContext);
			return View(appUserDTO);
		}
	}
}
