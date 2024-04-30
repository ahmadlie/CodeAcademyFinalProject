using System.ComponentModel.DataAnnotations;

namespace FinalProjectBase.Models
{
    public class AppUserViewModel
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "FirstName is Required!")]
		[MaxLength(30, ErrorMessage = "FirstName must have less than 30 characters!")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "LastName is Required!")]
		[MaxLength(30, ErrorMessage = "LastName must have less than 30 characters!")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email Address is Required!")]
		[EmailAddress(ErrorMessage = "Please enter valid Email Address.")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Phone Number is Required!")]
		[RegularExpression(@"^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$", ErrorMessage = "Phone Number is not valid.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Username is Required!")]
		[MinLength(8, ErrorMessage = "Username must have more than 8 characters!")]
		public string? Username { get; set; }
        public string? Password { get; set; }
        public ImageViewModel? Image { get; set; }
        public IFormFile? FormFile { get; set; }
        public List<AppRoleViewModel> AppRoles { get; set; }
        public List<string>? SelectedRoles { get; set; }
        public List<PostViewModel> postViewModels { get; set; }
    }
}
