using AutoMapper;
using BusinessLayer.Abstract.Base;
using DataAccessLayer.Repository.Abtract.Base;
using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class BaseService<TDto, TEntity, TRepository> : IBaseService<TDto>
		where TEntity : BaseEntity, new()
		where TRepository : IBaseRepository<TEntity>

	{
		public TRepository _repository { get; set; }
		public IMapper _mapper;
		public BaseService(TRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public void Create(TDto dto)
		{
			
			var entity = _mapper.Map<TEntity>(dto);
			_repository.Add(entity);
		}

		public void Delete(int id)
		{
			var dto = GetById(id);
			var entity = _mapper.Map<TEntity>(dto);
			_repository.Delete(entity);
			_repository.Save();	
		}

		public IEnumerable<TDto> GetAll()
		{
			var entities = _repository.GetAll();
			var dto = _mapper.Map<IEnumerable<TDto>>(entities);
			return dto;
		}

		public TDto GetById(int id)
		{
			var entity = _repository.GetById(id);
			var dto = _mapper.Map<TDto>(entity);
			return dto;
		}

		public async Task Update(TDto dto)
		{
			var entity = _mapper.Map<TEntity>(dto);
			_repository.Update(entity);
		}
	}
}
