using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
	public class PostDTO
	{
		public int Id { get; set; }
		public string Comment {  get; set; }
		public string Content { get; set; }
		public List<ImageDTO> Images { get; set; }
		public AppUserDTO AppUser { get; set; }
		public int? AppUserId { get; set; }
	}
}
