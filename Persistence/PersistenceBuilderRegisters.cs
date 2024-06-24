using Domain.RepositoryAbstracts.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;
using Persistence.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
	public static class PersistenceBuilderRegisters
	{
		public static void AddDbContext(this IHostApplicationBuilder builder)
		{
			builder.Services.AddDbContext<EStoreAppDb>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
			});
		}
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IAppUserRepository,AppUserRepository>();
			services.AddScoped<IAppRolesRepository,AppRolesRepository>();
			services.AddScoped<IAppUserRoleRepository,AppUserRoleRepository>();
			services.AddScoped<IAppUserTokenRepository,AppUserTokenRepository>();
			services.AddScoped<ICategoryRepository,CategoryRepository>();
			services.AddScoped<IInvoiceRepository,InvoiceRepository>();
			services.AddScoped<IInvoiceItemRepository,InvoiceItemRepository>();
			services.AddScoped<IProductRepository,ProductRepository>();
		}
	}
}
