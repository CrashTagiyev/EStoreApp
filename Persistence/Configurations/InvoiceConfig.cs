using Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
	public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
	{
		public void Configure(EntityTypeBuilder<Invoice> builder)
		{
			builder.HasOne(i => i.Cashier)
				.WithMany(u => u.CashierInvoices)
				.HasForeignKey(i => i.CashierId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(i => i.Customer)
				.WithMany(u => u.CustomerInvoices)
				.HasForeignKey(i => i.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
