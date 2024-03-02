using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.ViewComponents.Base;

public class FriendRequestViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}