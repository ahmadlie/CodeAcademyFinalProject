using FP.BusinessLayer.Abstract;
using FP.DTOLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Concrete;
using FP.BusinessLayer.Exceptions;
using PostmarkDotNet;

namespace FP.BusinessLayer.Concrete;
public class MailService : ISmsService
{
	private readonly IConfiguration _configuration;
	private readonly UserManager<AppUser> _userManager;
	public MailService(IConfiguration configuration,
					   UserManager<AppUser> userManager)
	{
		_configuration = configuration;
		_userManager = userManager;
	}

	public async Task<string> SendSmsAsync(ResetPasswordDTO dto)
	{
		var user = await _userManager.FindByNameAsync(dto.Username);
		if (user is not null)
		{
			if (user.Email != dto.EmailAddress) { throw new WrongEmailException(); }
			SmtpClient cl = new();
			cl.Host = "smtp.mail.ru";
			cl.Port = 587;
			cl.EnableSsl = true;
			cl.UseDefaultCredentials = false;
			cl.Credentials = new NetworkCredential(_configuration["Verification:Email"], _configuration["Verification:Pass"]);

			MailAddress from = new(_configuration["Verification:Email"]!);
			MailAddress to = new(dto.EmailAddress!);

			MailMessage message = new();
			message.From = from;
			message.To.Add(to);
			message.Subject = "Reset your Password with Verification Code!";
			var verificationCode = GenerateVerificationCode();
			message.Body = $"Your Verification Code : {verificationCode}";

			cl.Send(message);
			user.VerificationCode = verificationCode;
			var res = await _userManager.UpdateAsync(user);
			if (!res.Succeeded) { throw new Exception(); }
			return user.UserName!;
		}
		else { throw new UserNotFoundException(); }

	}

	private string GenerateVerificationCode()
	{
		return Guid.NewGuid().ToString().Substring(1, 6);
	}
}
