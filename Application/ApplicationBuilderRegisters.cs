using Application.Validators.Fluent;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public static class ApplicationBuilderRegisters
	{
		public static void AddCustomValidators(this IServiceCollection services) {
			services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<LoginRequestDTOValidator>();
		}
	}
}
