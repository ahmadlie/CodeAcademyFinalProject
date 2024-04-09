using AutoMapper;
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
using Microsoft.EntityFrameworkCore;
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
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ITokenService _tokenService;
		private readonly IHostingEnvironment _hostEnvironment;

		public UserService(IUserRepository repository,
			IMapper mapper,
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			IHostingEnvironment hostEnvironment,
			ITokenService tokenService,
			RoleManager<AppRole> roleManager)
		{
			_userRepository = repository;
			_mapper = mapper;
			_userManager = userManager;
			_signInManager = signInManager;
			_hostEnvironment = hostEnvironment;
			_tokenService = tokenService;
			_roleManager = roleManager;
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

		public async Task<string> Login(AppUserDTO appUserDTO)
		{
			var resUser = await _userManager.FindByNameAsync(appUserDTO.Username!);
			if (resUser is not null)
			{
				var resSignIn = await _signInManager.PasswordSignInAsync(resUser, appUserDTO.Password!, false, false);
				if (!resSignIn.Succeeded) { throw new Exception("Incorrect Username or Password!"); }
				var userDTO = _mapper.Map<AppUserDTO>(resUser);
				var rolenames = await _userManager.GetRolesAsync(resUser);
				foreach (var rolename in rolenames)
				{
					var role = await _roleManager.FindByNameAsync(rolename);
					userDTO.AppRoles.Add(_mapper.Map<AppRoleDTO>(role));
				}
				return _tokenService.CreateAccessToken(5, userDTO);
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
	}
}

