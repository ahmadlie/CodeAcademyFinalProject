using BusinessLayer.Abstract;
using BusinessLayer.Exceptions;
using FP.BusinessLayer.Abstract;
using FP.BusinessLayer.Exceptions;
using FP.DTOLayer;
using FP.UI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FP.UI.Controllers;
public class ResetPasswordController : Controller
{
	private readonly ISmsService _smsService;
	private readonly IUserService _userService;
	public ResetPasswordController(ISmsService smsService,
								   IUserService userService)
	{
		_smsService = smsService;
		_userService = userService;
	}

	public IActionResult Index()
	{
		return View();
	}


	[HttpPost]
	public async Task<IActionResult> SendSmsAsync(ResetPasswordDTO dto)
	{
		try
		{
			var userName = await _smsService.SendSmsAsync(dto);
			HttpContext.Response.Cookies.Append("UserIdentifier", userName);
			return RedirectToAction(nameof(VerifySmsCode));
		}
		catch (UserNotFoundException ex)
		{
			TempData["sms"] = ex.Message;
			return RedirectToAction(nameof(Index));
		}
		catch (WrongEmailException ex)
		{
			TempData["sms"] = ex.Message;
			return RedirectToAction(nameof(Index));
		}
		catch (Exception)
		{
			TempData["sms"] = MailErrors.Common;
			return RedirectToAction(nameof(Index));
		}
	}

	[HttpGet]
	public async Task<IActionResult> VerifySmsCode()
	{
		return View();
	}


	[HttpPost]
	public async Task<IActionResult> VerifySmsCode(ResetPasswordDTO dto)
	{
		try
		{
			var userName = HttpContext.Request.Cookies["UserIdentifier"];
			await _userService.VerifySmsCodeAsync(dto, userName!);
			return RedirectToAction(nameof(ResetPassword));
		}
		catch (NotCorrectException ex)
		{
			TempData["verifySms"] = ex.Message;
			return RedirectToAction(nameof(VerifySmsCode));
		}
		catch (Exception)
		{
			TempData["verifySms"] = MailErrors.Common;
			return RedirectToAction(nameof(VerifySmsCode));
		}
	}


	[HttpGet]
	public async Task<IActionResult> ResetPassword()
	{
		return View();
	}


	[HttpPost]
	public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
	{
		try
		{
			var userName = HttpContext.Request.Cookies["UserIdentifier"];
			await _userService.ResetPasswordAsync(dto, userName!);
			HttpContext.Response.Cookies.Delete("UserIdentifier");
			return RedirectToAction(controllerName: "Login", actionName: "Index");
		}
		catch (NotVerifiedException)
		{
			return RedirectToAction(nameof(SendSmsAsync));
		}
		catch (Exception)
		{
			return BadRequest(MailErrors.Common);
		}
	}
}
