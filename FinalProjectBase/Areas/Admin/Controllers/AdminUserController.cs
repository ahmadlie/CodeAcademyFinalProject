﻿using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using FinalProjectBase.Config;
using FinalProjectBase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FinalProjectBase.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminUserController : Controller
	{
		private readonly IUserService _userService;
		public AdminUserController(IUserService userService)
		{
			_userService = userService;
		}


		[HttpGet]
		public IActionResult Index()
		{
			var userDTOs = _userService.GetAll();
			var appUserViewModels = _userService.MapFromTo<IEnumerable<AppUserDTO>, IEnumerable<AppUserViewModel>>(userDTOs);
			return View(appUserViewModels);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(AppUserViewModel model)
		{
			model.Image = new ImageViewModel()
			{
				ImageName = model.FormFile?.FileName,
				ImageUrl = _userService.UploadUserPhoto(model.FormFile)
			};
			var appUserDTO = _userService.MapFromTo<AppUserViewModel, AppUserDTO>(model);
			try
			{
				await _userService.SignUp(appUserDTO);
				return RedirectToAction("Index", "AdminUser");
			}
			catch (Exception ex)
			{
				ViewData["CreateMessage"] = ex.Message;
				return RedirectToAction("Create");
			}			
		}


		[HttpPost]
		public IActionResult Delete([FromRoute] int id)
		{
			_userService.Delete(id);
			return RedirectToAction("Index");
		}


		[HttpGet]
		public IActionResult Update([FromRoute] int id)
		{
			var appUserDTO = _userService.GetById(id);
			var model = _userService.MapFromTo<AppUserDTO, AppUserViewModel>(appUserDTO);
			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> Update(AppUserViewModel model) 
		{
			model.Image = new ImageViewModel()
			{
				Id = model.Image.Id,
				ImageName = model.FormFile.FileName,
				ImageUrl = _userService.UploadUserPhoto(model.FormFile)
			};

			var userDTO = _userService.MapFromTo<AppUserViewModel, AppUserDTO>(model);
			try
			{
				await _userService.Update(userDTO);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				return RedirectToAction("Update", "AdminUser");
			}
			
		}
	}
}
