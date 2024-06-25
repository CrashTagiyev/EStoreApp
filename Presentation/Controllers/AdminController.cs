using Domain.DTOs.AppUserDTOs;
using Infrastructure.Services.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly AppUserServices _userServices;

		public AdminController(AppUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpPost("[action]")]
		[Authorize(Roles ="SuperAdmin")]
		public async Task<IActionResult> CreateAppUser(AppUserCreateDTO appUserCreateDTO)
		{
			var response=await _userServices.CreateAppUser(appUserCreateDTO);

			if (response.StatusCode == HttpStatusCode.OK)
				return Ok(response);

			return NotFound(response);
		}
	}
}
