using BusinessLayer.Abstract;
using DTOLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class AccountDetailsController : Controller
	{
		private readonly IUserService _userService;
		public AccountDetailsController(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			var userDTO = await _userService.GetCurrentUserAsync(HttpContext);
			return View(model: userDTO);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAsync(AppUserDTO dto) 
		{
			try
			{
				await _userService.UpdateUserDetails(dto);
				return RedirectToAction(actionName:"Index",controllerName:"AuthorProfile");
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(Index));
			}
			
		}
	}
}
