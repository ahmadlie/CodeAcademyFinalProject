using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Base;
using BusinessLayer.Exceptions;
using DataAccessLayer.Repository.Abtract;
using DataAccessLayer.Repository.Concrete;
using DTOLayer;
using EntityLayer.Concrete;
using FP.BusinessLayer.Exceptions;
using FP.DTOLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Encodings;
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
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IImageService _imageService;
		private readonly IHostingEnvironment _hostEnvironment;

		public UserService(IUserRepository repository,
			IMapper mapper,
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			IHostingEnvironment hostEnvironment,
			RoleManager<AppRole> roleManager,
			IImageService imageService)
		{
			_userRepository = repository;
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
			_hostEnvironment = hostEnvironment;
			_roleManager = roleManager;
			_imageService = imageService;
		}
		public int Create(AppUserDTO dto)
		{
			var entity = _mapper.Map<AppUser>(dto);
			return _userRepository.Add(entity);
		}

		public void Delete(int id)
		{
			var entity = _userRepository.GetById(id);
			_userRepository.Delete(entity);
		}

		public async Task<IEnumerable<AppUserDTO>> GetAll()
		{
			var responses = _userRepository.GetAll();
			var resDTOs = _mapper.Map<IEnumerable<AppUserDTO>>(responses);
			foreach (var dto in resDTOs)
			{
				var appUser = _userRepository.GetById(dto.Id);
				var roleNames = await _userManager.GetRolesAsync(appUser);
				foreach (var roleName in roleNames)
				{
					var appRole = await _roleManager.FindByNameAsync(roleName);
					dto.AppRoles = new List<AppRoleDTO>() { _mapper.Map<AppRoleDTO>(appRole) };
				}
			}
			return resDTOs;
		}

		public async Task<AppUserDTO> GetById(int id)
		{
			var entity = _userRepository.GetById(id);
			var dto = _mapper.Map<AppUserDTO>(entity);
			//var roleNames = await _userManager.GetRolesAsync(entity);

			var rolesInDb = _roleManager.Roles.ToList();
			dto.AppRoles = _mapper.Map<List<AppRoleDTO>>(rolesInDb);
			//foreach (var roleName in roleNames)
			//{ 
			//	var appRole = await _roleManager.FindByNameAsync(roleName);
			//	dto.AppRoles = new List<AppRoleDTO>()
			//	{
			//		_mapper.Map<AppRoleDTO>(appRole)
			//	};
			//}			
			return dto;
		}

		public async Task Login(AppUserDTO appUserDTO)
		{
			var resUser = await _userManager.FindByNameAsync(appUserDTO.Username!);
			if (resUser is not null)
			{
				var resSignIn = await _signInManager.PasswordSignInAsync(resUser, appUserDTO.Password!, false, false);
				if (!resSignIn.Succeeded) { throw new Exception("Incorrect Username or Password!"); }
			}
			else { throw new Exception("User Not Found!"); }
		}

		public AppUser MapForSignIn(AppUserDTO user)
		{
			return _mapper.Map<AppUser>(user);
		}

		public async Task SignUp(AppUserDTO user)
		{
			if (user.Image is null) { user.ImageId = 10; }
			var entity = _mapper.Map<AppUser>(user);
			var res = await _userManager.CreateAsync(entity, user.Password!);
			var roleResult = await _userManager.AddToRoleAsync(entity, "Member");
			if (!roleResult.Succeeded) { throw new Exception("Something Wrong!"); }
			if (!res.Succeeded) { throw new Exception("Something Wrong!"); }

		}

		public async Task Update(AppUserDTO dto)
		{
			var user = _mapper.Map<AppUser>(dto);
			var existingUser = await _userManager.FindByIdAsync($"{dto.Id}");
			existingUser!.PasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, dto.Password!);
			existingUser.Image = user.Image;
			existingUser.Email = user.Email;
			existingUser.FirstName = user.FirstName;
			existingUser.LastName = user.LastName;
			existingUser.PhoneNumber = user.PhoneNumber;
			existingUser.UserName = user.UserName;

			var newRoles = new List<AppRole>();

			foreach (var selectedRole in dto.SelectedRoles!)
			{
				newRoles.Add(await _roleManager.FindByNameAsync(selectedRole));
			}
			existingUser.AppRoles = newRoles;
			var result = await _userManager.UpdateAsync(existingUser);
			Console.WriteLine(result);
		}

		public string UploadUserPhoto(IFormFile formFile)
		{

			var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "Account");
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
			var userWithPosts = _userRepository.GetById(appUser!.Id);
			var appUserDTO = _mapper.Map<AppUserDTO>(userWithPosts);
			return appUserDTO;
		}

		public async Task UpdateUserWithPhoto(AppUserDTO dto)
		{
			if (dto.ImageId == 10)        //If user photo is defaultuserimage.jpg
			{
				var imageId = _imageService.Create(new ImageDTO()
				{
					ImageName = dto.FormFile!.FileName,
					ImageUrl = UploadUserPhoto(dto.FormFile)
				});

				var user = _userRepository.GetById(dto.Id);
				user.Image = null;
				user.ImageId = imageId;
				_userRepository.Update(user);

			}
			else
			{
				var imageDTO = new ImageDTO()
				{
					Id = dto.ImageId,
					ImageName = dto.FormFile!.FileName,
					ImageUrl = UploadUserPhoto(dto.FormFile)
				};
				await _imageService.Update(imageDTO);
			}

		}

		public async Task<AppUserDTO> SearchByUserNameAsync(string userName)
		{
			var user = await _userRepository.SearchByUserNameAsync(userName);
			var userDTO = _mapper.Map<AppUserDTO>(user);
			return userDTO;
		}

		public async Task<IEnumerable<AppUserDTO>> SearchByNameAsync(string name)
		{
			var users = await _userRepository.SearchByNameAsync(name);
			var usersDTO = _mapper.Map<IEnumerable<AppUserDTO>>(users);
			return usersDTO;
		}

		public async Task VerifySmsCodeAsync(ResetPasswordDTO dto, string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null) { throw new UserNotFoundException(); }
			if (user.VerificationCode != dto.VerificationCode) { throw new NotCorrectException(); }
			user.isSmsVerified = true;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) { throw new Exception(); }
		}

		public async Task ResetPasswordAsync(ResetPasswordDTO dto, string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user is null) { throw new UserNotFoundException(); }
			if (!user.isSmsVerified) { throw new NotVerifiedException(); }
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword!);
			if (!result.Succeeded) { throw new Exception(); }
		}

		public async Task UpdateUserDetails(AppUserDTO appUserDTO)
		{
			var user = _userRepository.GetById(appUserDTO.Id);
			////user.Email = appUserDTO.EMail;
			user.FirstName = appUserDTO.FirstName;
			user.LastName = appUserDTO.LastName;
			user.PhoneNumber = appUserDTO.PhoneNumber;
			user.UserName = appUserDTO.Username;
			//_userRepository.Update(user);
			await _userManager.UpdateAsync(user);
		}
	}
}

