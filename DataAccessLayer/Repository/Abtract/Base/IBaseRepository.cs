using EntityLayer.Base;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Abtract.Base
{
	public interface IBaseRepository<TEntity> where TEntity : BaseEntity ,  new()
	{
		IEnumerable<TEntity> GetAll();
		TEntity GetById(int id);
		void Delete(TEntity entity);
		void Update(TEntity entity);
		int Add(TEntity entity);
		void Save();
	}
}
