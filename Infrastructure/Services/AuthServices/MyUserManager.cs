using Application.Services.UserServices;
using Domain.DTOs.TokenDTOs;
using Domain.Entities.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Reflection.Metadata.Ecma335;


namespace Infrastructure.Services.AuthServices
{
	public class MyUserManager : IMyUserManager
	{
		private readonly EStoreAppDb _context;

		public MyUserManager(EStoreAppDb context)
		{
			_context = context;
		}


		public async Task<bool> CheckPassword(AppUser user, string password)
		{
			if (user.PasswordHash == password)
				return true;
			return false;
		}
		async public Task<ICollection<AppUser>> GetAllUsers()
		{
			return await _context.Users.ToListAsync();
		}
		public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
		{
			if (user == null)
				return IdentityResult.Failed(new IdentityError { Description = "User cannot be null." });

			try
			{
				user.CreatedTime = DateTime.UtcNow;
				await _context.Users.AddAsync(user, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
				return IdentityResult.Success;
			}
			catch (Exception ex)
			{
				return IdentityResult.Failed(new IdentityError { Description = ex.Message });
			}
		}

		public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
		{
			if (user == null)
				return IdentityResult.Failed(new IdentityError { Description = "User cannot be null." });

			try
			{
				user.DeletedTime = DateTime.UtcNow;
				_context.Users.Remove(user);
				await _context.SaveChangesAsync(cancellationToken);
				return IdentityResult.Success;
			}
			catch (Exception ex)
			{
				return IdentityResult.Failed(new IdentityError { Description = ex.Message });
			}
		}

		public void Dispose()
		{
		}

		public async Task<AppUser?> FindByIdAsync(int userId, CancellationToken cancellationToken)
		{

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
			return user;
		}

		public async Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == normalizedUserName, cancellationToken);
		}

		public async Task<AppUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress.Contains(normalizedEmail), cancellationToken);
		}

		public Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Username);
		}

		public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Id.ToString());
		}

		public Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Username);
		}

		public Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken)
		{
			user.Username = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken)
		{
			user.Username = userName;
			return Task.CompletedTask;
		}

		public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
		{
			if (user == null)
				return IdentityResult.Failed(new IdentityError { Description = "User cannot be null." });

			try
			{
				_context.Users.Update(user);
				await _context.SaveChangesAsync(cancellationToken);
				return IdentityResult.Success;
			}
			catch (Exception ex)
			{
				return IdentityResult.Failed(new IdentityError { Description = ex.Message });
			}
		}

		// IUserEmailStore<AppUser> methods
		public Task SetEmailAsync(AppUser user, string? email, CancellationToken cancellationToken)
		{
			user.EmailAddress = email;
			return Task.CompletedTask;
		}

		public Task<string?> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.EmailAddress);
		}

		public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.IsEmailConfirmed);
		}

		public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
		{
			user.IsEmailConfirmed = confirmed;
			return Task.CompletedTask;
		}

		public Task<string?> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.EmailAddress.ToUpper());
		}

		public Task SetNormalizedEmailAsync(AppUser user, string? normalizedEmail, CancellationToken cancellationToken)
		{
			user.EmailAddress = normalizedEmail;
			return Task.CompletedTask;
		}

		// IUserRoleStore<AppUser> methods
		public async Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
		{
			if (user == null || string.IsNullOrWhiteSpace(roleName))
				throw new ArgumentNullException(user == null ? nameof(user) : nameof(roleName));

			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
			if (role != null)
			{
				_context.UserRoles.Add(new AppUserRole { UserId = user.Id, RoleId = role.Id });
				await _context.SaveChangesAsync(cancellationToken);
			}
		}

		public async Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
		{
			if (user == null || string.IsNullOrWhiteSpace(roleName))
				throw new ArgumentNullException(user == null ? nameof(user) : nameof(roleName));

			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
			if (role != null)
			{
				var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id, cancellationToken);
				if (userRole != null)
				{
					_context.UserRoles.Remove(userRole);
					await _context.SaveChangesAsync(cancellationToken);
				}
			}
		}

		public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			var roles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.Role).Select(r => r.Name).ToListAsync(cancellationToken);

			return roles;
		}

		public async Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
		{
			if (user == null || string.IsNullOrWhiteSpace(roleName))
				throw new ArgumentNullException(user == null ? nameof(user) : nameof(roleName));

			var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
			if (role != null)
			{
				return await _context.UserRoles.AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id, cancellationToken);
			}
			return false;
		}

		public async Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(roleName))
				throw new ArgumentNullException(nameof(roleName));

			var users = from ur in _context.UserRoles
						join u in _context.Users on ur.UserId equals u.Id
						join r in _context.Roles on ur.RoleId equals r.Id
						where r.Name == roleName
						select u;

			return await users.ToListAsync(cancellationToken);
		}

		public async Task CreateAppUserToken(AppUser user, TokenDTO tokenDTO)
		{
			_context.UserTokens.Add(new AppUserToken()
			{
				User = user,
				Token = tokenDTO.Token,
				TokenType = tokenDTO.TokenType.ToString(),
				ExpireTime = tokenDTO.ExpireDate

			});
			await _context.SaveChangesAsync();

		}

		public async Task<AppUserToken> FindAppUserToken(AppUser user, string token)
		{
			var userToken =await _context.UserTokens.FirstOrDefaultAsync(ut => ut.UserId == user.Id && ut.Token == token);
			return userToken;
		}

		public async Task<AppUser> FindUserByToken(string token)
		{
			var appUserToken=await  _context.UserTokens.FirstOrDefaultAsync(ut=>ut.Token== token);
			var user =await _context.Users.FirstOrDefaultAsync(u => u.Id == appUserToken.UserId);
			return user;
		}
	}
}


