using Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AppUserDTOs
{
	public class AppUserGetDTO
	{
        public int Id { get; set; }
        public string Username { get; set; }
		public string EmailAddress { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public virtual ICollection<string> Roles { get; set; }




	}
}
