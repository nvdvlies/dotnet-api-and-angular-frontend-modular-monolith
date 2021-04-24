﻿using Demo.Domain.Invoice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Persistence.Configurations
{
    public class InvoiceLineEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.ToTable(nameof(InvoiceLine))
                .HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.SellingPrice)
                .IsRequired();

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            //builder.HasOne(x => x.Item)
            //    .WithMany(x => x.InvoiceLines)
            //    .HasForeignKey(x => x.ItemId)
            //    .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRowVersion();
        }
    }
}
