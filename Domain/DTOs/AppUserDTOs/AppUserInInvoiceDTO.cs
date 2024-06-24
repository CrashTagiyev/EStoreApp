using Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AppUserDTOs
{
	public class AppUserInInvoiceDTO
	{
		public string Username { get; set; }
		public string EmailAddress { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public virtual ICollection<AppUserRole> UserRoles { get; set; }
		//

	}
}
