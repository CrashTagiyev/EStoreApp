using Domain.Entities.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryAbstracts
{
	public interface IRepository<T> where T : Entity, new()
	{
		
		Task CreateAsync(T entity);
		Task<ICollection<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
	}
}
