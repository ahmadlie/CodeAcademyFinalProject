using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.DTOLayer;
public class ResetPasswordDTO
{
	public string? Username { get; set; }
	public string? EmailAddress { get; set; }
	public string? VerificationCode { get; set; }
	public string? NewPassword { get; set; }

	[Compare("NewPassword",ErrorMessage = "Password are not equal!")]
	public string? RepeatPassword { get; set; }
}
