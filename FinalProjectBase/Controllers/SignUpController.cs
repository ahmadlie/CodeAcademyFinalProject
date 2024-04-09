using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FinalProjectBase.Controllers
{
	[AllowAnonymous]
	public class SignUpController : Controller
	{
		private readonly IUserService _userService;

		public SignUpController(IUserService userService)
		{

			_userService = userService;

		}

		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public async Task<IActionResult> SignUp([FromForm] SignUpViewModel signUpViewModel)
		{
			AppUserDTO appUserDTO = new AppUserDTO()
			{
				FirstName = signUpViewModel.FirstName,
				LastName = signUpViewModel.LastName,
				PhoneNumber = signUpViewModel.PhoneNumber,
				EMail = signUpViewModel.Mail,
				Username = signUpViewModel.Username,
				Password = signUpViewModel.Password,
			};

			try
			{
				await _userService.SignUp(appUserDTO);
				return RedirectToAction("Index", "Login");
			}
			catch (Exception)
			{
				return RedirectToAction("Index");
			}



		}
	}
}
