using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.CategoryDTOs
{
	public class CategoryResponse
	{
		public string ResponseMessage { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}
}
