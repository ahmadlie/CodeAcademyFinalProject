﻿using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectBase.Controllers
{
	[Authorize]
	public class AuthorProfileController : Controller
	{
		private readonly IUserService _userService;
		private readonly IUAboutService _uAboutService;
		private readonly IImageService _imageService;
		public AuthorProfileController(IUserService userService,
			IUAboutService uAboutService,
			IImageService imageService)
		{
			_userService = userService;
			_uAboutService = uAboutService;
			_imageService = imageService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var appUserDTO = await _userService.GetCurrentUserAsync(HttpContext);
			return View(appUserDTO);
		}


		[HttpPost]
		public async Task<IActionResult> AddAbout(UAboutDTO uAboutDTO)
		{
			try
			{
				_uAboutService.Create(uAboutDTO);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> UpdateUAbout(UAboutDTO uAboutDTO)
		{
			try
			{
				await _uAboutService.Update(uAboutDTO);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost]
		public async Task<IActionResult> UpdateUserImage(AppUserDTO dto)
		{
			try
			{				
				await _userService.UpdateUserWithPhoto(dto);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			

		}
	}
}
