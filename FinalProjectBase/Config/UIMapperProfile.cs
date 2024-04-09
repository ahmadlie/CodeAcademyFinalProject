using AutoMapper;
using DTOLayer;
using FinalProjectBase.Models;

namespace FinalProjectBase.Config
{
	public class UIMapperProfile : Profile
	{
		public UIMapperProfile() 
		{
			CreateMap<AppUserViewModel,AppUserDTO>();
			CreateMap<AppUserDTO, AppUserViewModel>();

		    CreateMap<AppUserDTO , LoginViewModel>();
		    CreateMap<LoginViewModel, AppUserDTO>();

			CreateMap<ImageViewModel, ImageDTO>();
			CreateMap<ImageDTO, ImageViewModel>();

			CreateMap<AppRoleDTO, AppRoleViewModel>();
			CreateMap<AppRoleViewModel, AppRoleDTO>();

			CreateMap<PostViewModel, PostDTO>();
			CreateMap<PostDTO, PostViewModel>();
		}
	}
}
