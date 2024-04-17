﻿using BusinessLayer.Abstract.Base;
using DTOLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IImageService : IBaseService<ImageDTO>
	{
		Task<ImageDTO> FindLast();
	}
}
