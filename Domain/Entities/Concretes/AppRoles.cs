using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class AppRoles:Entity
	{
        public string Name { get; set; }
        public string NormalizedName { get; set; }
		public virtual ICollection<AppUserRole> UserRoles { get; set; }

	}
}
