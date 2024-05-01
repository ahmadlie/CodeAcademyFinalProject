using BusinessLayer.Abstract;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;

namespace FinalProjectBase.Controllers
{
	[Authorize]
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
		public async Task<IActionResult> SearchUser(SearchViewModel model)
		{
			SearchResultViewModel vm = new();

			if (model.Type == "username" || model.Type == null)
			{
				var userDTO = await _userService.SearchByUserNameAsync(model.Text);
				if (userDTO is not null)
				{
					vm.User = userDTO;
					return View(viewName: "Index", model: vm);
				}
				else { return RedirectToAction(controllerName: "Errors", actionName: "NotFoundPage"); }
			}
			var userDTOs = await _userService.SearchByNameAsync(model.Text);
			if (userDTOs.Any())
			{
				vm.UserDTOs = userDTOs;
				return View(viewName: "Index", model: vm);
			}
			else { return BadRequest(HttpStatusCode.NotFound); }
		}
	}
}
