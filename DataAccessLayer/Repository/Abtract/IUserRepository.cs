using DataAccessLayer.Repository.Abtract.Base;
using DataAccessLayer.Repository.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Abtract
{
	public interface IUserRepository 
	{
		IEnumerable<AppUser> GetAll();
		AppUser GetById(int id);
		void Delete(AppUser entity);
		void Update(AppUser entity);
		void Add(AppUser entity);
		void Save();
	}
}
