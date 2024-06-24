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
	public class InvoiceItemRepository : GenericRepository<InvoiceItems>, IInvoiceItemRepository
	{
		public InvoiceItemRepository(EStoreAppDb context) : base(context)
		{
		}

		public async Task CreateAsync(InvoiceItems entity)
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

		public async Task DeleteInRange(ICollection<InvoiceItems> invoiceItems)
		{
			_context.RemoveRange(invoiceItems);
			await SaveChanges();
		}

		public async Task<ICollection<InvoiceItems>> GetAllAsync()
		{
			return await _table.ToListAsync();
		}

		public async Task<InvoiceItems> GetByIdAsync(int id)
		{
			return await _table.FindAsync(id);
		}

		public async Task UpdateAsync(InvoiceItems entity)
		{
			_table.Update(entity);
			await SaveChanges();
		}
	}
}
