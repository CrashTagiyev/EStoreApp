using Domain.DTOs.RegistrationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Fluent
{
	public class RegistrationRequestDTOValidator : AbstractValidator<RegistrationRequestDTO>
	{
		public RegistrationRequestDTOValidator()
		{
			RuleFor(rr => rr.Email)
				.NotEmpty()
				.EmailAddress()
				.NotNull();

			RuleFor(rr => rr.Password)
				.NotEmpty()
				.NotNull();

			RuleFor(rr => rr.ConfirmPassword)
				.NotEmpty()
				.NotNull()
				.Equal(rr => rr.Password);

			RuleFor(rr => rr.Username)
				.NotNull()
				.NotEmpty();

			RuleFor(rr => rr.Firstname)
				.NotNull()
				.NotEmpty();

			RuleFor(rr => rr.Lastname)
				.NotNull()
				.NotEmpty();
		
		}
	}
}
