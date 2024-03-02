using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.ViewComponents.Base
{
	public class CreatePostViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
