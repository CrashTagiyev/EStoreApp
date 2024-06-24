using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class InvoiceItems : Entity
	{
		public int Quantity { get; set; }

		//Foreign keys
		public int ProductId { get; set; }
		public int InvoiceId { get; set; }

		//Navigation properties
		public virtual Product Product { get; set; }
		public virtual Invoice Invoice { get; set; }

	}
}
