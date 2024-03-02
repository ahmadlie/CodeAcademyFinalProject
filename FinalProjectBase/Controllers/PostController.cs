using BusinessLayer.Abstract;
using DataAccessLayer.Repository.Abtract;
using DTOLayer;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBase.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
		}

        
		public IActionResult Index()
		{
			return View();
		}

        [HttpGet]

		public IActionResult Create(){
			return View();
		}
         
		[HttpPost]
		public IActionResult Create([FromForm] PostViewModel postViewModel)
		{
			PostDTO postDTO = new PostDTO()
			{
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
