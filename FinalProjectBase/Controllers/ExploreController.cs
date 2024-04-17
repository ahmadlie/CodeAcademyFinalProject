using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	public class ExploreController : Controller
	{
		private readonly IUserService _userService;

		public ExploreController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> SearchUser(string userName)
		{
			var userDTO = await _userService.SearchByUserNameAsync(userName);
			return View(viewName: "Index", model: userDTO);
		}
	}
}
