using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceDTOs.InvoiceRequests
{
    public class InvoiceCreateDTO
    {
        public int CashierId { get; set; }
        public int CustomerId { get; set; }
    }
}
