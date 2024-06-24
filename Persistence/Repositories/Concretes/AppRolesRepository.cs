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
	public class AppRolesRepository : GenericRepository<AppRoles>, IAppRolesRepository
	{
		public AppRolesRepository(EStoreAppDb context) : base(context)
		{
		}

		public async Task CreateAsync(AppRoles entity)
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

		public async Task<ICollection<AppRoles>> GetAllAsync()
		{
			return await _table.Include(ar=>ar.UserRoles).ToListAsync();
		}

		public async Task<AppRoles> GetByIdAsync(int id)
		{
			return await _table!.Include(ar=>ar.UserRoles).FirstOrDefaultAsync(ar=>ar.Id==id)!;
		}

		public async Task UpdateAsync(AppRoles entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}

}
