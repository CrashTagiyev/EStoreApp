using Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Generics
{
	public class GenericRepository<T> where T : Entity, new()
	{
		protected readonly EStoreAppDb _context;
		protected readonly DbSet<T> _table;

		public GenericRepository(EStoreAppDb context)
		{
			_context = context;
			_table = _context.Set<T>();
		}


        protected  async Task SaveChanges() { 
			await _context.SaveChangesAsync();
		}

    }
}
