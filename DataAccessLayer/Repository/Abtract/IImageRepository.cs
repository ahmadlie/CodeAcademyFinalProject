﻿using DataAccessLayer.Repository.Abtract.Base;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Abtract
{
	public interface IImageRepository : IBaseRepository<Image>
	{
		Task<Image> FindLast();
	}
}
