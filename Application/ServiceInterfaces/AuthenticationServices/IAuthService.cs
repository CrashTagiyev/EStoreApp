using Domain.DTOs.LoginDTOs;
using Domain.DTOs.RegistrationDTOs;
using Domain.DTOs.TokenDTOs;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthenticationServices
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<RegisterResponseDTO> Registration(RegistrationRequestDTO request);
        Task<string> VeryfyEmail( int userId,string token);
        Task<TokenDTO> ForgotPassword(string email);
         Task<string> ChangeForgottenPassword(string token, string newPassword);
	}
}
