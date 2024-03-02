using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class AppUser : IdentityUser<int>
	{
		public string? FirstName {  get; set; }
		public string? LastName { get; set; }
		public List<Post>? Posts { get; set; }
		public List<Image>? Images {  get; set; }
		public List<Comment>? Comments { get; set; }

	}
}
