using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Repository.Abtract;
using DTOLayer;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class ImageService : BaseService<ImageDTO, Image, IImageRepository> , IImageService 
	{
		public ImageService(IImageRepository repository, IMapper mapper) : base(repository, mapper)
		{
		}

		public async Task<ImageDTO> FindLast()
		{
			return _mapper.Map<ImageDTO>(await _repository.FindLast());
		}
	}
}
