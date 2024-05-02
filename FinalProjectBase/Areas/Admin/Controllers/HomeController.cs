using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class HomeController : Controller
	{
		//public IActionResult Index()
		//{
		//	return View();
		//}
	}
}
