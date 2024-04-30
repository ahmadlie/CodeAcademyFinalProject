using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[EmailAddress(ErrorMessage = "Please enter valid Email Address.")]
		public string? EMail { get; set; }

		[RegularExpression(@"^\(994\)\s{1}(50|51|55|70|77)\s{1}\d{3}\s{1}\d{2}\s{1}\d{2}$", ErrorMessage = "Phone Number is not valid.")]
		public string? PhoneNumber { get; set; } 
		public string? Username { get; set; } 
		public string? Password { get; set; }
		public ImageDTO? Image { get; set; }
		public int ImageId { get; set; }
		public List<AppRoleDTO> AppRoles { get; set; }
		public List<string>? SelectedRoles { get; set; }
		public List<PostDTO> Posts { get; set; }
		public List<UAboutDTO> UAbouts { get; set; }
		public IFormFile? FormFile { get; set; }
		public string? SecurityToken { get; set; }
	}
}
