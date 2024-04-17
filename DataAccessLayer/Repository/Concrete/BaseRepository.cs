using DataAccessLayer.Repository.Abtract.Base;
using EntityLayer.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Concrete
{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
	{
		public AppDbContext _dbContext;
		public DbSet<TEntity> _entities;

		public BaseRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			_entities = _dbContext.Set<TEntity>();			
		}

		public int Add(TEntity entity)
		{
			_entities.Add(entity);
			_dbContext.SaveChanges();
			return entity.Id;
		}

		public void Delete(TEntity entity)
		{
			_entities.Remove(entity);
			//_dbContext.SaveChanges();

		}

		public IEnumerable<TEntity> GetAll()
		{
			return _entities.ToList();
		}

		public TEntity GetById(int id)
		{
			return _entities.AsNoTracking().FirstOrDefault(X => X.Id == id);
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			_entities.Update(entity);
			_dbContext.SaveChanges();
		}
	}
}
