using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Base
{
	public interface IBaseService<TDto>
	{
		TDto GetById(int id);
		IEnumerable<TDto> GetAll();
		void Update(TDto dto);
		void Delete(TDto dto);
		void Create(TDto dto);

	}
}
