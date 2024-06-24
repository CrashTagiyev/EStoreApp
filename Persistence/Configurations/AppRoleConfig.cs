using Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
	public class AppRoleConfig : IEntityTypeConfiguration<AppRoles>
	{
		public void Configure(EntityTypeBuilder<AppRoles> builder)
		{
			builder.HasData(
			new AppRoles
			{
				Id = 1,
				CreatedTime = DateTime.UtcNow,
				Name = "SuperAdmin",
				NormalizedName = "SUPERADMIN",
			},
			new AppRoles
			{
				Id = 2,
				CreatedTime = DateTime.UtcNow,
				Name = "Admin",
				NormalizedName = "ADMIN",
			},
			new AppRoles
			{
				Id = 3,
				CreatedTime = DateTime.UtcNow,
				Name = "Cashier",
				NormalizedName = "CASHIER",
			},
			new AppRoles
			{
				Id = 4,
				CreatedTime = DateTime.UtcNow,
				Name = "Customer",
				NormalizedName = "CUSTOMER",
			}
		);
		}
	}
}
