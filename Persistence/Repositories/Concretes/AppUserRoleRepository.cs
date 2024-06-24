using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Concretes
{
	public class AppUserRoleRepository(EStoreAppDb context) : GenericRepository<AppUserRole>(context), IAppUserRoleRepository
	{
		public async Task CreateAsync(AppUserRole entity)
		{
			await _table.AddAsync(entity);
			await SaveChanges();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await _table.FindAsync(id);
			if (entity != null)
			{
				_table.Remove(entity);
				await SaveChanges();
			}
		}

		public async Task<ICollection<AppUserRole>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<AppUserRole> GetByIdAsync(int id)
		{
			return await _table.FindAsync(id);
		}

		public async Task<ICollection<string>> GetUserRoles(AppUser user)
		{
			var roles= user.UserRoles.Select(x => x.Role).ToList();
			return roles.Select(r=>r.Name).ToList();
		}

		public async Task UpdateAsync(AppUserRole entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}

}
