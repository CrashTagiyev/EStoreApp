using Application.Services.AuthenticationServices;
using Application.Services.UserServices;
using Domain.DTOs.LoginDTOs;
using Domain.DTOs.RegistrationDTOs;
using Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class AccountController(IAuthService authService, IMyUserManager myUserManager) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
		private readonly IMyUserManager _myUserManager = myUserManager;

		[HttpPost("[action]")]
		public async Task<IActionResult> LogIn(LoginRequestDTO loginRequestDTO)
		{
			if (!ModelState.IsValid)
				return ValidationProblem(ModelState);

			var result = await _authService.Login(loginRequestDTO);


			if (result.StatusCode == HttpStatusCode.NotFound)
				return NotFound("User Tapilmadi");

			if (result.StatusCode == HttpStatusCode.MethodNotAllowed)
				return BadRequest("Email activation is not confirmed");

			if (result.StatusCode == HttpStatusCode.Unauthorized)
				return Unauthorized("Sifre yanlisdir");


			return Ok(result);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> Registration(RegistrationRequestDTO registerDTO)
		{
			if (ModelState.IsValid)
			{
				var response = await _authService.Registration(registerDTO);
				if (response.StatusCode == HttpStatusCode.OK)
					return Ok(response.Message);
				return BadRequest(response.Message);
			}
			return BadRequest(registerDTO);

		}

		[HttpGet("[action]")]
		public async Task<IActionResult> VerifyEmailCOnfirmation(int userId, string token)
		{
			var response = await _authService.VeryfyEmail(userId, token);
			if (response.StartsWith("SUCCESS"))
				return Ok(response);

			return BadRequest(response);
		}


		[HttpPost("[action]")]
		public async Task<IActionResult> ForgotPassword([EmailAddress] string email)
		{
			var token = await _authService.ForgotPassword(email);

			if (token == null)
				return BadRequest("Email incorrect");

			return Ok(token);
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> ChangeForgottenPassword(string rePasstoken, string newPassword)
		{
			var response = await _authService.ChangeForgottenPassword(rePasstoken, newPassword);
			if (response.StartsWith("FAILED"))
				return BadRequest(response);

			return Ok(response);
		}



		[Authorize(Roles = "SuperAdmin")]
		[HttpPost("[action]")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _myUserManager.FindByIdAsync(id, new());
			if (user is not null)
			{
				await _myUserManager.DeleteAsync(user, new());
				return Ok();
			}
			return BadRequest("User did not found");
		}

		[Authorize(Roles = "SuperAdmin,Admin")]
		[HttpPost("[action]")]
		public async Task<IActionResult> ShowUsers()
		{
			var users = await _myUserManager.GetAllUsers();
			if (users is not null)
			{
				return Ok(users);
			}
			return BadRequest("Users did not found");
		}
	}
}
