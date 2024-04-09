using BusinessLayer.Abstract;
using DTOLayer;
using EntityLayer.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateAccessToken(int minute, AppUserDTO appUser)
		{

			// Creating symmetricKey
			SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));

			// SymmetricKey encoding with additional algorithm
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			// Creating User Claims
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
				new Claim(ClaimTypes.Name,appUser.FirstName + " " + appUser.LastName),
				new Claim(ClaimTypes.MobilePhone,appUser.PhoneNumber!),
			};
			foreach (var role in appUser.AppRoles!) { claims.Add(new Claim(ClaimTypes.Role, role.Name)); }

			// Creating Token Object and add value in here
			JwtSecurityToken jwtSecurityToken = new(
				issuer: _configuration["Token:Issuer"],
				audience: _configuration["Token:Audience"],
				//claims: claims, 
				expires: DateTime.Now.AddMinutes(minute),
				signingCredentials: signingCredentials
			);

			// Get instance from this class for create Token
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			return handler.WriteToken(jwtSecurityToken);
		}
	}
}
