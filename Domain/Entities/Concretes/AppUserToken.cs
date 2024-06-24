using Domain.Entities.Abstracts;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class AppUserToken:Entity
	{
		public string TokenType { get; set; }
		public string Token { get; set; }
		public DateTime ExpireTime { get; set; }

		//
		public int UserId { get; set; }
		public virtual AppUser User { get; set; }

	}
}
