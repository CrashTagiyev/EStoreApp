using Domain.DTOs.LoginDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Fluent
{
	public class LoginRequestDTOValidator:AbstractValidator<LoginRequestDTO>
	{
        public LoginRequestDTOValidator()
        {
            RuleFor(lr=>lr.EmailAddress)
                .NotEmpty()
				.NotNull()
                .EmailAddress().WithMessage("Email is not correct");

			RuleFor(lr => lr.PasswordHash)
			 .NotEmpty()
			 .NotNull();

		}
	}
}
