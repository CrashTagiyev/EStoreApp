using Domain.DTOs.ProductDTOs.ProductRequestsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceItemsDTOs
{
	public class InvoiceItemsGetDTO
	{
		public ProductGetDTO Product { get; set; }
		public int Quantity { get; set; }
	}
}
