﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
	public class ImageDTO
	{
		public int Id {  get; set; }
		public string ImageName { get; set; }
		public string ImageUrl { get; set; }
		public List<AppUserDTO> AppUsers { get; set; }	
	}
}
