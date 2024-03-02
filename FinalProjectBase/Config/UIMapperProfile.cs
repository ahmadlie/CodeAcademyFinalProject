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
			CreateMap<ImageViewModel, ImageDTO>();
			CreateMap<ImageDTO, ImageViewModel>();
		}
	}
}
