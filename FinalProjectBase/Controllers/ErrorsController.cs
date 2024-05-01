using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class ErrorsController : Controller
	{
		public IActionResult AccessDenied()
		{
			return View();
		}

		public IActionResult NotFoundPage()
		{
			return View();
		}
	}
}
