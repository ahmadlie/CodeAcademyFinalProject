﻿using Microsoft.AspNetCore.Identity;
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
		public Image? Image {  get; set; }
		public int? ImageId { get; set; }
		public List<Comment>? Comments { get; set; }
		public List<AppRole>? AppRoles { get; set; }

	}
}
