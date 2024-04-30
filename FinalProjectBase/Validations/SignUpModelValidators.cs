using FinalProjectBase.Models;
using FluentValidation;

namespace FinalProjectBase.Validations;
public class SignUpModelValidators : AbstractValidator<SignUpViewModel>
{
	public SignUpModelValidators()
	{
		RuleFor(x => x.Username).NotEmpty().WithMessage("Username is Required!")
							    .MinimumLength(8).WithMessage("Username must more than 8 characters!");

		RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is Required!")
								.MaximumLength(30).WithMessage("FirstName must less than 30 characters!");

		RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is Required!")
								.MaximumLength(30).WithMessage("LastName must less than 30 characters!");

		RuleFor(x => x.PhoneNumber)
		   .Matches(@"^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$")
		   .WithMessage("Phone Number is not valid.");

		RuleFor(x => x.Mail)
		   .NotEmpty().WithMessage("Email Address is Required!")
		   .EmailAddress().WithMessage("Please enter valid Email Address.");

	}
}
