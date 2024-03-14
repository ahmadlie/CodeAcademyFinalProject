using BusinessLayer.Abstract.Base;
using DTOLayer;
using EntityLayer.Concrete;
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
		Task<string> Login(AppUserDTO user);
		string UploadUserPhoto(IFormFile formFile);
		TDest MapFromTo<TSrc, TDest>(TSrc src);
		Task<AppUserDTO> GetCurrentUserAsync(HttpContext httpContext);
	}
}
