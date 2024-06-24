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
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(EStoreAppDb context) : base(context)
		{
		}

		public async Task CreateAsync(Category entity)
		{
			await _table.AddAsync(entity);
			await SaveChanges();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_table.Remove(entity);
				await SaveChanges();
			}
		}

		public async Task<ICollection<Category>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<Category> GetByIdAsync(int id)
		{
			var category= await _table.FirstOrDefaultAsync(c=>c.Id == id);
			return category;
		}

	

		public async Task UpdateAsync(Category entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}
}
