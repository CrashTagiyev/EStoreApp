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
	public class AppUserConfig : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasData(
			new AppUser
			{
				Id = 1,
				EmailAddress = "CrashTagiyev3@gmail.com",
				FirstName = "Emil",
				LastName = "Tagiyev",
				Username = "SuperAdmin",
				PasswordHash = "123qweA@",
			});

			builder.Property(u => u.EmailAddress).IsRequired();
		}
	}
}
