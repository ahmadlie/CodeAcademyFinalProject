using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{ 

	[Authorize(Roles = "SuperAdmin")]
	public class ErrorsController : Controller
	{
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
