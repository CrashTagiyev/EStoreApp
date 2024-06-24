using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.TokenDTOs
{
	public class TokenDTO
	{
		public string Token { get; set; }
		public TokenType TokenType { get; set; }
		public DateTime ExpireDate { get; set; }= DateTime.UtcNow.AddDays(1); // 1 day
	}
}
