using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Repository.Abtract;
using DTOLayer;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectBase.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class AdminPostController : Controller
	{

		private readonly IPostService _postService;
		public AdminPostController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			IEnumerable<PostDTO> posts = _postService.GetPosts();
			List<PostViewModel> postViewModels = posts.Select(postDTO =>
			new PostViewModel()
			{
				Id = postDTO.Id,
				Images = postDTO.Images.Select(imageDTO => new ImageViewModel()
				{
					Id = imageDTO.Id,
					ImageName = imageDTO.ImageName,
					ImageUrl = imageDTO.ImageUrl,
				}
				).ToList(),
				Content = postDTO.Content,
			}
			).ToList();

			return View(postViewModels);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(PostViewModel postViewModel)
		{
			if(postViewModel.FormFiles is null) { return BadRequest("Please Select Image"); }
			PostDTO postDTO = new PostDTO()
			{
				Content = postViewModel.Content,
				Id = postViewModel.Id,
				Images = postViewModel.FormFiles.Select(file => new ImageDTO()
				{
					ImageName = file.FileName,
					ImageUrl = _postService.UploadPhoto(file)
				}).ToList()
			};
			_postService.Create(postDTO);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Update([FromRoute] int id)
		{
			var postDTO = _postService.GetPostById(id);

			PostViewModel postViewModel = new PostViewModel()
			{
				Id = postDTO.Id,
				Content = postDTO.Content,
				Images = postDTO.Images.Select(imageDTO => new ImageViewModel()
				{
					Id = imageDTO.Id,
					ImageName = imageDTO.ImageName,
					ImageUrl = imageDTO.ImageUrl
				}).ToList()
			};

			return View(postViewModel);
		}


		[HttpPost]
		public IActionResult Update(PostViewModel postViewModel , params int[] changedImageIds)
		{
			if (postViewModel.FormFiles is null) { return BadRequest("Please Select Image"); }
			PostDTO postDTO = new PostDTO()
			{
				Id = postViewModel.Id,
				Content = postViewModel.Content,
				Images = postViewModel.FormFiles.Select((file,index) => new ImageDTO()
				{
					Id = changedImageIds[index],
					ImageName = file.FileName,
					ImageUrl = _postService.UploadPhoto(file),
				}).ToList()
			};		

			_postService.Update(postDTO);
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public IActionResult Delete([FromRoute] int id)
		{
			_postService.Delete(id);
			return RedirectToAction(nameof(Index));
		}



	}
}
