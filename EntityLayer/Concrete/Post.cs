using EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Post : BaseEntity
	{
		public string? Content { get; set; }		
		public int? AppUserId { get; set; }
		public AppUser? AppUser { get; set; }
		public List<Image>? Images { get; set; }
		public List<Comment>? Comments { get; set; }


	}
}
