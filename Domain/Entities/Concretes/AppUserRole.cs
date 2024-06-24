using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class AppUserRole:Entity
	{
		public int UserId { get; set; }
		public virtual AppUser User { get; set; }
		public int RoleId { get; set; }
		public virtual AppRoles Role { get; set; }
	}
}
