using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Image : BaseEntity
	{
		public string ImageUrl { get; set; } = null!;
		public string ImageName { get; set; } = null!;
		public int? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
		public int?	PostId { get; set; }
		public Post? Post { get; set; }
	}
}
