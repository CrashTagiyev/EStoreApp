using Domain.DTOs.AppUserDTOs;
using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
    public class AppUser:Entity
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
		public bool IsEmailConfirmed { get; set; }

		public virtual ICollection<AppUserRole> UserRoles { get; set; }
		public virtual ICollection<AppUserToken> UserTokens { get; set; }

		//
		public virtual ICollection<Invoice> CashierInvoices { get; set; }
		public virtual ICollection<Invoice> CustomerInvoices { get; set; }


		public static explicit operator AppUserGetDTO(AppUser appUser)
		{
			return new AppUserGetDTO
			{
				Id = appUser.Id,
				Username = appUser.Username,
				EmailAddress = appUser.EmailAddress,
				FirstName = appUser.FirstName,
				LastName = appUser.LastName,
				Roles = appUser.UserRoles.Select(ur => ur.Role.Name).ToList()
			};
		}
	}
}
