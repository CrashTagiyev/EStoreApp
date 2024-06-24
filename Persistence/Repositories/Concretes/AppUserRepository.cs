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
	public class AppUserRepository(EStoreAppDb context) : GenericRepository<AppUser>(context), IAppUserRepository
	{
		public async Task CreateAsync(AppUser entity)
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

		public async Task<ICollection<AppUser>> GetAllAsync()
		{
			return await _table
				.Include(u => u.CashierInvoices)
				.Include(u => u.UserTokens)
				.Include(u => u.UserRoles)
				.ToListAsync();
		}

		public async Task<AppUser> GetByIdAsync(int id)
		{
			return await _table.Include(u => u.CashierInvoices)
				.Include(u => u.UserTokens)
				.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == id);
		}

		public async Task UpdateAsync(AppUser entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}
}
