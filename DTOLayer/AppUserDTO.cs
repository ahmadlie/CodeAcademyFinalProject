using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer
{
	public class AppUserDTO
	{
		public int Id {  get; set; }
		public string? FirstName { get; set; } 
		public string? LastName { get; set; } 
		public string? EMail { get; set; } 
		public string? PhoneNumber { get; set; } 
		public string? Username { get; set; } 
		public string? Password { get; set; }
		public ImageDTO? Image { get; set; }
		public List<AppRoleDTO> AppRoles { get; set; }
		public List<string>? SelectedRoles { get; set; }
		public List<PostDTO> Posts { get; set; }
		public List<UAboutDTO> UAbouts { get; set; }
	}
}
