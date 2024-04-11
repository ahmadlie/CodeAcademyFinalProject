using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class UAbout : BaseEntity
	{
		public string? Text { get; set; }
		public int? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
	}
}
