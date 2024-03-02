using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FinalProjectBase.Controllers
{
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
			AppUserDTO appUserDTO = new AppUserDTO()
			{
				Username = loginViewModel.Username,
				Password = loginViewModel.Password,
			};
			try
			{
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
