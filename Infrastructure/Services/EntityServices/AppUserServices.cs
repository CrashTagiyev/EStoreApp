using Application.Services.UserServices;
using Domain.DTOs.AppUserDTOs;
using Domain.DTOs.InvoiceDTOs;
using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.EntityServices
{
	public class AppUserServices
	{
		private readonly IAppUserRepository _appUserRepository;
		private readonly IAppUserRoleRepository _appUserRoleRepository;
		private readonly IMyUserManager _myUserManager;

		public AppUserServices(IAppUserRepository appUserRepository, IAppUserRoleRepository appUserRoleRepository, IMyUserManager myUserManager)
		{
			_appUserRepository = appUserRepository;
			_appUserRoleRepository = appUserRoleRepository;
			_myUserManager = myUserManager;
		}

		public async Task<AppUserGetDTO> GetAppUser(int userId)
		{
			var user = await _appUserRepository.GetByIdAsync(userId);
			if (user is not null)
			{
				var roles = await _appUserRoleRepository.GetUserRoles(user);
				var userDTO = new AppUserGetDTO()
				{
					Id = userId,
					Username = user.Username,
					EmailAddress = user.EmailAddress,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Roles = roles,
				};
				return userDTO;
			}
			return null!;
		}

		public async Task<AppUserResponse> CreateAppUser(AppUserCreateDTO appUserCreateDTO)
		{
			var newUser = new AppUser
			{
				Username = appUserCreateDTO.Username,
				EmailAddress = appUserCreateDTO.EmailAddress,
				PasswordHash = appUserCreateDTO.PasswordHash,
				FirstName = appUserCreateDTO.FirstName,
				LastName = appUserCreateDTO.LastName,
				IsEmailConfirmed = true,
			};
			await _appUserRepository.CreateAsync(newUser);
			var user = await _myUserManager.FindByEmailAsync(newUser.EmailAddress,new());
			await _myUserManager.AddToRoleAsync(user!, appUserCreateDTO.Role, new());
			return new AppUserResponse() { ResponseMessage = "SUCCESS:AppUser created successfully", StatusCode = HttpStatusCode.OK };

		}

	}
}
