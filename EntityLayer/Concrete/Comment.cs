using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Comment  : BaseEntity
	{
		public string Content { get; set; } = null!;
		public int AppUserId { get; set; }
		public AppUser? AppUser { get; set; }

		public int PostId { get; set; }
		public Post? Post { get; set; }
	}
}
