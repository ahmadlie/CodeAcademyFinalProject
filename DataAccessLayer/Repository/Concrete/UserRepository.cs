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
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _dbContext;
		private readonly DbSet<AppUser> _dbSet;
		public UserRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<AppUser>();
		}

		public int Add(AppUser entity)
		{
			_dbSet.Add(entity);
			_dbContext.SaveChanges();
			return entity.Id;
		}

		public void Delete(AppUser entity)
		{
			_dbSet.Remove(entity);
			_dbContext.SaveChanges();
		}

		public IEnumerable<AppUser> GetAll()
		{
			return _dbSet.Include(x => x.Image)
				.Include(x => x.AppRoles)
				.ToList();
		}

		public AppUser GetById(int id)
		{
			return _dbSet.Include(x => x.Image)
				.Include(x => x.UAbouts)
				.Include(x => x.Posts)
				.ThenInclude(x => x.Images)
				.AsNoTracking()
				.FirstOrDefault(x => x.Id == id);
		}

		public int Save()
		{
			return _dbContext.SaveChanges();
		}

		public void Update(AppUser entity)
		{
			_dbSet.Update(entity);
			_dbContext.SaveChanges();
		}


		public async Task<AppUser> SerachByUserNameAsync(string userName)
		{
			var user = _dbSet.Where(u => u.UserName == userName).Include(u=> u.Image).FirstOrDefault();	
			if (user is not null) { return user; }
			else { return null; }
		}
	}
}
