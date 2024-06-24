﻿using Domain.Entities.Concretes;
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
	public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
	{
		public void Configure(EntityTypeBuilder<AppUserRole> builder)
		{
			builder
				.HasKey(ur => new { ur.UserId, ur.RoleId });

			builder
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(ur => ur.UserId);

			builder
				.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId);
			
			builder.HasData(
				new AppUserRole
				{
					UserId = 1,
					RoleId = 1
				});
		}
	}
}
