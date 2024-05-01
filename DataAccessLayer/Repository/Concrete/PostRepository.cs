using DataAccessLayer.Repository.Abtract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Concrete
{
	public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		public PostRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public Post GetPostById(int id)
		{
			return _entities.Include(x => x.Images).Where(x => x.Id == id).FirstOrDefault();
		}

		public IEnumerable<Post> GetPosts()
		{
			return _entities.Include(x => x.Images)
					.Include(x => x.AppUser).ThenInclude(y => y.Image)
					.ToList();


		}


	}
}
