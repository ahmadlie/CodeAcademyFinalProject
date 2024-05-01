using BusinessLayer.Abstract;
using DataAccessLayer.Repository.Abtract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class PostController : Controller
	{
		private readonly IPostService _postService;
		private readonly IUserService _userService;
		public PostController(IPostService postService, IUserService userService)
		{
			_postService = postService;
			_userService = userService;
		}


		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(PostViewModel postViewModel)
		{
			var appUserDTO = await _userService.GetCurrentUserAsync(HttpContext);
			PostDTO postDTO = new PostDTO()
			{
				AppUserId = appUserDTO.Id,
				Content = postViewModel.Content,
				Images = postViewModel.FormFiles.Select(formFile => new ImageDTO()
				{
					ImageName = formFile.FileName,
					ImageUrl = _postService.UploadPhoto(formFile)
				}).ToList()
			};

			_postService.Create(postDTO);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult ShowPost()
		{
			var posts = _postService.GetPosts();
			return View(posts);
		}

	}
}
