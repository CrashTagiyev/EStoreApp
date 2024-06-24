using Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryAbstracts.Concretes
{
	public interface IAppUserRoleRepository:IRepository<AppUserRole>
	{
		Task<ICollection<string>> GetUserRoles(AppUser user);
	}
}
