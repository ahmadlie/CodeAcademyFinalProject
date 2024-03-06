using BusinessLayer.Abstract.Base;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IPostService : IBaseService<PostDTO>
	{
		string UploadPhoto(IFormFile image);
		IEnumerable<PostDTO> GetPosts();
		PostDTO GetPostById(int id);
	}
}
