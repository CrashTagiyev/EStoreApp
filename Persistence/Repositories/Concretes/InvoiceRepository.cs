using Domain.Entities.Concretes;
using Domain.RepositoryAbstracts.Concretes;
using Persistence.Data;
using Persistence.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Concretes
{
	public class InvoiceRepository(EStoreAppDb context) : GenericRepository<Invoice>(context), IInvoiceRepository
	{
		public async Task CreateAsync(Invoice entity)
		{
			_table.Add(entity);
			await SaveChanges();
		}

		public async Task DeleteAsync(int id)
		{
			var entity = _table.Find(id);
			if (entity != null)
			{
				_table.Remove(entity);
				await SaveChanges();
			}
		}

		public Task<ICollection<Invoice>> GetAllAsync()
		{
			var result = _table.ToList();
			return Task.FromResult((ICollection<Invoice>)result);
		}

		public Task<Invoice> GetByIdAsync(int id)
		{
			var entity = _table.Find(id);
			return Task.FromResult(entity);
		}

		public async Task UpdateAsync(Invoice entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}
}
