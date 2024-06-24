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
	internal class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		public ProductRepository(EStoreAppDb context) : base(context)
		{
		}

		public async Task CreateAsync(Product entity)
		{
			try
			{
				await _table.AddAsync(entity);
				await SaveChanges();
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public async Task DeleteAsync(int id)
		{
			try
			{
				var product = await _table.FirstOrDefaultAsync(p => p.Id == id);
				if (product != null)
				{

					_table.Remove(product);
					await SaveChanges();
				}
				else throw new ArgumentNullException();
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}

		public async Task<ICollection<Product>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<ICollection<Product>> GetAllProductsEagerLoadingAsync()
		{
			return await _table.Include(p => p.Category).ToListAsync();
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			try
			{
				var product = await _table.FirstOrDefaultAsync(p => p.Id == id);
				return product;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
		}
		public async Task UpdateAsync(Product entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}
}
