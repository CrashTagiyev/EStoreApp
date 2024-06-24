using Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Concretes
{
	public class Invoice:Entity
	{
		public string InvoiceBarCode { get; set; }
		public string BarCode { get; set; }
		
		
		//
		public int CashierId {  get; set; }
		public int CustomerId {  get; set; }


		//
        public virtual AppUser Cashier { get; set; }
        public virtual AppUser Customer { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }
	}
}
