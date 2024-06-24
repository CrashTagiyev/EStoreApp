using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum TokenType
	{
		AccessToken=1,
		RefreshToken,
		RePasswordToken,
		EmailConfirmToken
	}
}
