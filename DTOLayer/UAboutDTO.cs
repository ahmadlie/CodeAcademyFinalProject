using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
	public class UAboutDTO
	{
		public int Id {  get; set; }
		public string? Text { get; set; }
		public int? AppUserId { get; set; }
		public AppUserDTO? AppUser { get; set; }
	}
}
