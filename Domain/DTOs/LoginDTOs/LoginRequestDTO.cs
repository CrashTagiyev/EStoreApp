﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.LoginDTOs
{
    public class LoginRequestDTO
    {
        public string? EmailAddress { get; set; }
        public string? PasswordHash { get; set; }
    }
}
