using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class Product : Entity
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }

		public virtual  Category Category { get; set; }
		public virtual  ICollection<InvoiceItems> InvoiceItems { get; set; }
	}
}
