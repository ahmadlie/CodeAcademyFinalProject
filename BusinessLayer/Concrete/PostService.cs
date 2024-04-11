using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Repository.Abtract;
using DataAccessLayer.Repository.Abtract.Base;
using DataAccessLayer.Repository.Concrete;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Concrete
{
	public class PostService : BaseService<PostDTO, Post, IPostRepository>, IPostService
	{
		private readonly IHostingEnvironment _hostEnvironment;
		private readonly IImageRepository _imageRepository;

		public PostService(IPostRepository repository, IMapper mapper, IHostingEnvironment hostEnvironment, IImageRepository imageRepository) : base(repository, mapper)
		{
			_hostEnvironment = hostEnvironment;
			_imageRepository = imageRepository;
		}

		public PostDTO GetPostById(int id)
		{
			var post = _repository.GetPostById(id);
			var postDto = _mapper.Map<PostDTO>(post);
			return postDto;
		}

		public IEnumerable<PostDTO> GetPosts()
		{
			var posts = _repository.GetPosts();
			return _mapper.Map<IEnumerable<PostDTO>>(posts);
		}

	
		public string UploadPhoto(IFormFile formfile)
		{
			var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "posts");
			var fileName = Path.GetFileName(formfile.FileName);
			var fullPath = Path.Combine(filePath, fileName);

			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				formfile.CopyToAsync(stream);
			}

			return fullPath;
		}
	}
}
