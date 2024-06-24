using Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
	public class AppUserTokenConfig : IEntityTypeConfiguration<AppUserToken>
	{
		public void Configure(EntityTypeBuilder<AppUserToken> builder)
		{
			builder
			.HasOne(ut => ut.User)
			.WithMany(u => u.UserTokens)
			.HasForeignKey(ut => ut.UserId);

		}
	}
}
