using Application.ServiceInterfaces.EmailServices;
using Application.Services.AuthenticationServices;
using Application.Services.UserServices;
using Domain.DTOs;
using Domain.DTOs.LoginDTOs;
using Domain.DTOs.RegistrationDTOs;
using Domain.DTOs.TokenDTOs;
using Domain.Entities.Concretes;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AuthServices
{
	public class AuthService : IAuthService
	{

		private readonly IMyUserManager _myUserManager;
		private readonly ITokenService _tokenService;
		private readonly IEmailService _emailService;
		public AuthService(IMyUserManager myUserManager, ITokenService tokenService, IEmailService emailService)
		{
			_myUserManager = myUserManager;
			_tokenService = tokenService;
			_emailService = emailService;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO dto)
		{
			var user = await _myUserManager.FindByEmailAsync(dto.EmailAddress!, new CancellationToken());
			if (user is null)
				return new LoginResponseDTO() { StatusCode = HttpStatusCode.NotFound };

			if (!user.IsEmailConfirmed)
				return new LoginResponseDTO() { StatusCode = HttpStatusCode.MethodNotAllowed };

			var result = await _myUserManager.CheckPassword(user, dto.PasswordHash);
			if (!result)
				return new LoginResponseDTO() { StatusCode = HttpStatusCode.Unauthorized };

			var roles = await _myUserManager.GetRolesAsync(user, new CancellationToken());
			var tokenRequestDto = new TokenRequestDto
			{
				Email = dto.EmailAddress,
				Id = user.Id.ToString(),
				UserName = user.Username,
				Roles = roles.ToList(),
				Claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, dto.EmailAddress),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, string.Join(",", roles.ToList()))
			}
			};

			var accessToken = _tokenService.GenerateAccessToken(tokenRequestDto);

			return new LoginResponseDTO
			{
				AccessToken = accessToken,
				StatusCode = HttpStatusCode.OK
			};
		}

		public async Task<RegisterResponseDTO> Registration(RegistrationRequestDTO request)
		{
			var isUserExist = await _myUserManager.FindByEmailAsync(request.Email!, new());
			if (isUserExist != null)
				return new RegisterResponseDTO() { Message = "User with this email is already exist", StatusCode = HttpStatusCode.BadRequest };

			var newUser = new AppUser()
			{
				Username = request.Username!,
				EmailAddress = request.Email!,
				FirstName = request.Firstname!,
				LastName = request.Lastname!,
				PasswordHash = request.Password!
			};

			var result = await _myUserManager.CreateAsync(newUser, new());
			if (result.Succeeded)
			{
				var user = await _myUserManager.FindByEmailAsync(request.Email!, new());
				await _myUserManager.AddToRoleAsync(user!, "Customer", new());
				var token = _tokenService.GenerateEmailConfirmToken(request.Email);
				var confirmationLink = $"http://localhost:5248/api/Account/VerifyEmailCOnfirmation?UserId={user!.Id}&Token={token}";

				await _myUserManager.CreateAppUserToken(user, new() { Token = token, TokenType = TokenType.EmailConfirmToken });
				await _emailService.SendEmailAsync(request.Email!, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>{confirmationLink}</a>;.", true);

				return new RegisterResponseDTO() { Message = "Account successfully created.Please Confirm from your email", StatusCode = HttpStatusCode.OK };
			}

			return new RegisterResponseDTO() { Message = "Invalid data", StatusCode = HttpStatusCode.BadRequest };
		}

		public async Task<string> VeryfyEmail(int userId, string token)
		{
			var user = await _myUserManager.FindByIdAsync(userId, new());
			if (user is not null)
			{
				var userToken = await _myUserManager.FindAppUserToken(user, token);
				if (userToken is not null)
				{
					if (userToken.ExpireTime < DateTime.Now)
						return "FAILED:Token expire time ended";

					user.IsEmailConfirmed = true;
					var updateResult = await _myUserManager.UpdateAsync(user!, new());
					if (updateResult.Succeeded)
						return "SUCCESS:Email successfully veryfied";
				}
				return "FAILED:Email confirm token did not found";
			}
			return "FAILED:Something is wrong";
		}


		public async Task<TokenDTO> ForgotPassword(string email)
		{
			var user = await _myUserManager.FindByEmailAsync(email, new());
			if (user is null)
				return null;

			var token = _tokenService.GenerateRePasswordToken(user.EmailAddress);
			token.ExpireDate.AddDays(1);
			await _myUserManager.CreateAppUserToken(user, token);

			return token;
		}

		public async Task<string> ChangeForgottenPassword(string token, string newPassword)
		{
			var user = await _myUserManager.FindUserByToken(token);
			if (user is null)
				return "FAILED:User with this token did not found or expired";

			var appUserToken = await _myUserManager.FindAppUserToken(user, token);
			if (appUserToken.ExpireTime < DateTime.UtcNow)
				return "FAILED:Token is expired";

			user.PasswordHash = newPassword;
			await _myUserManager.UpdateAsync(user, new());
			return "SUCCESS:Password successfully changed";
		}
	}
}
