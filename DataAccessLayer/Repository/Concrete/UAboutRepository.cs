using DataAccessLayer.Repository.Abtract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Concrete
{
	public class UAboutRepository : BaseRepository<UAbout>, IUAboutRepository
	{
		public UAboutRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
