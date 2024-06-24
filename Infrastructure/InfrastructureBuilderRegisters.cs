using Application.ServiceInterfaces.EmailServices;
using Application.Services.AuthenticationServices;
using Application.Services.UserServices;
using Infrastructure.Services.AuthServices;
using Infrastructure.Services.EmailServices;
using Infrastructure.Services.EntityServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public static class InfrastructureBuilderRegisters
	{
		public static void AddCustomServices(this IServiceCollection service)
		{
			//with interfaces
			service.AddScoped<IMyUserManager, MyUserManager>();
			service.AddScoped<IAuthService, AuthService>();
			service.AddScoped<ITokenService, TokenService>();
			service.AddScoped<IEmailService, EmailService>();

			//regular
			service.AddScoped<CategoryServices>();
			service.AddScoped<ProductService>();
			service.AddScoped<InvoiceServices>();
			service.AddScoped<AppUserServices>();

		}
	}
}
