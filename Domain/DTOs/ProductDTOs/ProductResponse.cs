﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ProductDTOs
{
	public class ProductResponse
	{
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
