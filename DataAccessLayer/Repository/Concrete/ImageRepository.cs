using DataAccessLayer.Repository.Abtract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Concrete
{
	public class ImageRepository : BaseRepository<Image> , IImageRepository
	{
		public ImageRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
