using AutoMapper;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Helper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<PostDTO, Post>();
			CreateMap<Post, PostDTO>();
			CreateMap<Image, ImageDTO>();
			CreateMap<ImageDTO, Image>();
			CreateMap<AppUser, AppUserDTO>()
			.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
			.ForMember(dest => dest.EMail, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
			CreateMap<AppUserDTO, AppUser>()
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
			.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EMail))
			.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

		}
	}
}
