﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Base;
using DataAccessLayer.Repository.Abtract;
using DataAccessLayer.Repository.Concrete;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IHostingEnvironment _hostEnvironment;

		public UserService(IUserRepository repository,
			IMapper mapper,
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			IHostingEnvironment hostEnvironment,
			ITokenService tokenService)
		{
			_userRepository = repository;
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
			_hostEnvironment = hostEnvironment;
			_tokenService = tokenService;
		}
		public void Create(AppUserDTO dto)
		{
			var entity = _mapper.Map<AppUser>(dto);
			_userRepository.Add(entity);
		}

		public void Delete(int id)
		{
			var entity = _userRepository.GetById(id);
			_userRepository.Delete(entity);
		}

		public IEnumerable<AppUserDTO> GetAll()
		{
			var res = _userRepository.GetAll();
			var resDTO = _mapper.Map<IEnumerable<AppUserDTO>>(res);
			return resDTO;
		}

		public AppUserDTO GetById(int id)
		{
			var entity = _userRepository.GetById(id);
			var dto = _mapper.Map<AppUserDTO>(entity);
			return dto;
		}

		public async Task<string> Login(AppUserDTO appUserDTO)
		{
			var resUser = await _userManager.FindByNameAsync(appUserDTO.Username!);
			if (resUser is not null)
			{
				var resSignIn = await _signInManager.PasswordSignInAsync(resUser, appUserDTO.Password!, false, false);
				if (!resSignIn.Succeeded) { throw new Exception("Incorrect Username or Password!"); }
				return _tokenService.CreateAccessToken(5, resUser);
			}
			else { throw new Exception("User Not Found!"); }
		}

		public AppUser MapForSignIn(AppUserDTO user)
		{
			return _mapper.Map<AppUser>(user);
		}

		public async Task SignUp(AppUserDTO user)
		{
			var entity = _mapper.Map<AppUser>(user);
			var res = await _userManager.CreateAsync(entity, user.Password!);
			if (!res.Succeeded) { throw new Exception("Something Wrong!"); }

		}

		public async Task Update(AppUserDTO dto)
		{
			var user = await _userManager.FindByIdAsync($"{dto.Id}");
			if (user is not null)
			{
				var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password!);
				user.Email = dto.EMail;
				user.FirstName = dto.FirstName;
				user.LastName = dto.LastName;
				user.PhoneNumber = dto.PhoneNumber;
				user.UserName = dto.Username;
				user.PasswordHash = newPasswordHash;
				var res = await _userManager.UpdateAsync(user);
				if (!res.Succeeded) { throw new Exception("Not Updated"); }
			}

		}

		public string UploadUserPhoto(IFormFile formFile)
		{

			var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "users");
			var fileName = Path.GetFileName(formFile.FileName);
			var fullPath = Path.Combine(filePath, fileName);

			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				formFile.CopyToAsync(stream);
			}

			return fullPath;
		}

		public TDest MapFromTo<TSrc, TDest>(TSrc src)
		{
			return _mapper.Map<TDest>(src);
		}


		public async Task<AppUserDTO> GetCurrentUserAsync(HttpContext httpContext)
		{
			var appUser = await _userManager.GetUserAsync(httpContext.User);
			var appUserDTO = _mapper.Map<AppUserDTO>(appUser);
			return appUserDTO;
		}


	}
}

