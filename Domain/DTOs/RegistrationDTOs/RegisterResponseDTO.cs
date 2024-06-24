using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.RegistrationDTOs
{
	public class RegisterResponseDTO
	{
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
