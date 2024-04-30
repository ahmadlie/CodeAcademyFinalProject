
using System.ComponentModel.DataAnnotations;

namespace FinalProjectBase.Models
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "Username is Required!")]
		[MinLength(8, ErrorMessage = "Username must have more than 8 characters!")]
		public string Username { get; set; } = null!;

		[Required(ErrorMessage = "FirstName is Required!")]
		[MaxLength(30, ErrorMessage = "FirstName must have less than 30 characters!")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "LastName is Required!")]
		[MaxLength(30, ErrorMessage = "LastName must have less than 30 characters!")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Email Address is Required!")]
		[EmailAddress(ErrorMessage = "Please enter valid Email Address.")]
		public string Mail { get; set; } = null!;

		[Required(ErrorMessage = "Phone Number is Required!")]
		[RegularExpression(@"^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$", ErrorMessage = "Phone Number is not valid.")]
		public string PhoneNumber { get; set; } = null!;

		[Required(ErrorMessage = "Password is Required!")]
		public string Password { get; set; } = null!;
	}
}
