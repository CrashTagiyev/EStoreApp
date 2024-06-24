using Domain.DTOs.ProductDTOs.ProductRequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceItemsDTOs
{
	public class InvoiceItemsCreateDTO
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
