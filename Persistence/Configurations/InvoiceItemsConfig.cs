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
	public class InvoiceItemsConfig : IEntityTypeConfiguration<InvoiceItems>
	{
		public void Configure(EntityTypeBuilder<InvoiceItems> builder)
		{
			builder.HasOne(ii => ii.Invoice)
				.WithMany(i => i.InvoiceItems)
				.HasForeignKey(ii => ii.InvoiceId);
			
			builder.HasOne(ii=>ii.Product)
				.WithMany(p=>p.InvoiceItems)
				.HasForeignKey(ii=>ii.ProductId);
		}

	}
}
