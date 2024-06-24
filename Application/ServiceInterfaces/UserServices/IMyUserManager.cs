using Domain.DTOs.LoginDTOs;
using Domain.DTOs.TokenDTOs;
using Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserServices
{
	public interface IMyUserManager
	{
		Task<bool> CheckPassword(AppUser user, string password);
		 Task<ICollection<AppUser>> GetAllUsers();
		Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken);

		Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken);

		void Dispose();

		Task<AppUser?> FindByIdAsync(int userId, CancellationToken cancellationToken);

		Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);

		Task<AppUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken);

		Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken);

		Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken);

		Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken);

		Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken);

		Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken);

		Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken);

		Task SetEmailAsync(AppUser user, string? email, CancellationToken cancellationToken);

		Task<string?> GetEmailAsync(AppUser user, CancellationToken cancellationToken);

		Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken);

		Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken);

		Task<string?> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken);

		Task SetNormalizedEmailAsync(AppUser user, string? normalizedEmail, CancellationToken cancellationToken);

		Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken);

		Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken);

		Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken);

		Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken);

		Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken);
		Task CreateAppUserToken(AppUser user,TokenDTO tokenDTO);

		Task<AppUserToken> FindAppUserToken(AppUser user,string token);
		Task<AppUser> FindUserByToken(string token);
	}
}
