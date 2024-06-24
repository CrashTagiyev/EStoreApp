using Domain.DTOs.AppUserDTOs;
using Domain.DTOs.InvoiceItemsDTOs;
using Domain.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.InvoiceDTOs.InvoiceRequests
{
    public class InvoiceGetDTO
    {
        public string InvoiceBarCode { get; set; }
        public string BarCode { get; set; }

        public virtual AppUserGetDTO Cashier { get; set; }
        public virtual AppUserGetDTO Customer { get; set; }
        public virtual ICollection<InvoiceItemsGetDTO> InvoiceItems { get; set; }
    }
}
