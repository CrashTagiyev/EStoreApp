using Domain.DTOs.LoginDTOs;
using Domain.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthenticationServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(TokenRequestDto dto);
        string GenerateRefreshToken();
        string GenerateEmailConfirmToken(string email);
        TokenDTO GenerateRePasswordToken(string email);
	}
}
