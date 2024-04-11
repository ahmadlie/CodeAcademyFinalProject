using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Base;
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
	public class UAboutService : BaseService<UAboutDTO, UAbout, IUAboutRepository>, IUAboutService
	{
		public UAboutService(IUAboutRepository repository, IMapper mapper) : base(repository, mapper)
		{
		}

		
	}
}
