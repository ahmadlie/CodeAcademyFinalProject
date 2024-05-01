using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FinalProjectBase.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
		{		
			try
			{
				var appUserDTO = _userService.MapFromTo<LoginViewModel, AppUserDTO>(loginViewModel);
				await _userService.Login(appUserDTO);
				return RedirectToAction("Index", "Home");
				
			}
			catch (Exception ex)
			{
				TempData["Message"] = ex.Message;
				return RedirectToAction("Index");
			}
		}
	}
}
