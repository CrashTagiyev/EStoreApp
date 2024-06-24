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
	public class AppUserTokenRepository : GenericRepository<AppUserToken>, IAppUserTokenRepository
	{
		public AppUserTokenRepository(EStoreAppDb context) : base(context)
		{
		}

		public async Task CreateAsync(AppUserToken entity)
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

		public async Task<ICollection<AppUserToken>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<AppUserToken> GetByIdAsync(int id)
		{
			return await _table.FindAsync(id);
		}

		public async Task UpdateAsync(AppUserToken entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}

}
