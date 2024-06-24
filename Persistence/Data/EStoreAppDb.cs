using Domain.Entities.Abstracts;
using Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
	public class EStoreAppDb:DbContext
	{

        public EStoreAppDb(DbContextOptions<EStoreAppDb> options)
            :base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{


			modelBuilder.ApplyConfiguration(new AppRoleConfig());
			modelBuilder.ApplyConfiguration(new AppUserConfig());
			modelBuilder.ApplyConfiguration(new AppUserRoleConfig());
			modelBuilder.ApplyConfiguration(new AppUserTokenConfig());
			modelBuilder.ApplyConfiguration(new ProductConfig());
			modelBuilder.ApplyConfiguration(new CategoryConfig());
			modelBuilder.ApplyConfiguration(new InvoiceItemsConfig());
			modelBuilder.ApplyConfiguration(new InvoiceConfig());

			modelBuilder.Entity<AppUserRole>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<AppUser>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<AppUserToken>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<AppRoles>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<Category>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<Invoice>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<InvoiceItems>().HasQueryFilter(e => !e.IsDeleted);
			modelBuilder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies(true);
		}

		public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRoles> Roles { get; set; }
		public DbSet<AppUserRole> UserRoles { get; set; }
		public DbSet<AppUserToken> UserTokens { get; set; }
		public DbSet<InvoiceItems> InvoiceItems { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

		public override int SaveChanges()
		{
			HandleSoftDelete();
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			HandleSoftDelete();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void HandleSoftDelete()
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.State == EntityState.Deleted && entry.Entity is Entity entity)
				{
					entry.State = EntityState.Modified;
					entity.IsDeleted = true;
				}
			}
		}

	}
}
