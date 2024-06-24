using Application.Services.AuthenticationServices;
using Domain.DTOs.LoginDTOs;
using Domain.DTOs.TokenDTOs;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AuthServices
{
	public class TokenService(IConfiguration configuration) : ITokenService
	{
		public string GenerateAccessToken(TokenRequestDto dto)
		{
			var jwtAccessToken = new JwtSecurityToken(
				issuer: configuration["JWT:Issuer"],
				audience: configuration["JWT:Audience"],
				claims: dto.Claims,
				expires: DateTime.Now.AddMinutes(30),

				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)), SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(jwtAccessToken);
		}

		public string GenerateEmailConfirmToken(string email)
		{
			using (var rng = RandomNumberGenerator.Create())
			{
				byte[] randomData = new byte[32];
				rng.GetBytes(randomData);
				string randomString = Convert.ToBase64String(randomData);

				string rawToken = email + randomString + DateTime.UtcNow.ToString();
				byte[] rawTokenBytes = Encoding.UTF8.GetBytes(rawToken);

				using (SHA256 sha256 = SHA256.Create())
				{
					byte[] hashBytes = sha256.ComputeHash(rawTokenBytes);
					string token = Convert.ToBase64String(hashBytes);


					return token;
				}

			}
		}
		public TokenDTO GenerateRePasswordToken(string email)
		{
			using (var rng = RandomNumberGenerator.Create())
			{
				byte[] randomData = new byte[32];
				rng.GetBytes(randomData);
				string randomString = Convert.ToBase64String(randomData);

				string rawToken = email + randomString + DateTime.UtcNow.ToString();
				byte[] rawTokenBytes = Encoding.UTF8.GetBytes(rawToken);

				using (SHA256 sha256 = SHA256.Create())
				{
					byte[] hashBytes = sha256.ComputeHash(rawTokenBytes);
					string token = Convert.ToBase64String(hashBytes);


					return new TokenDTO() {

						Token = token,
						TokenType=TokenType.RePasswordToken,
						ExpireDate=DateTime.UtcNow.AddDays(1),
					};
				}

			}
		}

		public string GenerateRefreshToken()
		{
			return Guid.NewGuid().ToString().ToLower();
		}
	}
}
