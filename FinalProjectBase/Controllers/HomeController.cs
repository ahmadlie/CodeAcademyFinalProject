using BusinessLayer.Abstract;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IPostService _postService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IPostService postService)
		{
			_logger = logger;
			_postService = postService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var posts = _postService.GetPosts().ToList();
			List<PostViewModel> postsViewModels = posts.Select(postDTO=> new PostViewModel()
			{
				Content = postDTO.Content,
				Images = postDTO.Images.Select(imageDTO=> new ImageViewModel()
				{
					ImageName = imageDTO.ImageName,
					ImageUrl = imageDTO.ImageUrl,
				}).ToList(),
			}).ToList();
			return View(postsViewModels);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
