using BusinessLayer.Abstract.Base;
using DTOLayer;
using EntityLayer.Concrete;
using FP.DTOLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IUserService : IBaseService<AppUserDTO>
	{
		Task SignUp(AppUserDTO user);
		AppUser MapForSignIn(AppUserDTO user);
		Task Login(AppUserDTO user);
		string UploadUserPhoto(IFormFile formFile);
		TDest MapFromTo<TSrc, TDest>(TSrc src);
		Task<AppUserDTO> GetCurrentUserAsync(HttpContext httpContext);
		Task UpdateUserWithPhoto(AppUserDTO user);
		Task<AppUserDTO> SearchByUserNameAsync(string userName);
		Task<IEnumerable<AppUserDTO>> SearchByNameAsync(string name);
		Task VerifySmsCodeAsync(ResetPasswordDTO dto , string userName);
		Task ResetPasswordAsync(ResetPasswordDTO dto , string userName);
		Task UpdateUserDetails(AppUserDTO appUserDTO);

	}
}
