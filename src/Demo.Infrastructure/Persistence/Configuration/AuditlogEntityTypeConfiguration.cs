﻿using Demo.Domain.Auditlog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Persistence.Configurations
{
    public class AuditlogEntityTypeConfiguration : IEntityTypeConfiguration<Auditlog>
    {
        public void Configure(EntityTypeBuilder<Auditlog> builder)
        {
            builder.ToTable(nameof(Auditlog))
                .HasKey(x => x.Id);

            builder.Property(t => t.EntityName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.EntityId)
                .IsRequired();

            builder.Property(t => t.ModifiedBy)
                .IsRequired();

            builder.Property(t => t.ModifiedOn)
                .IsRequired();

            builder.Property(p => p.Timestamp)
                .IsRowVersion();

            builder.HasMany(x => x.AuditlogItems)
                .WithOne(x => x.Auditlog)
                .HasForeignKey(x => x.AuditlogId);
        }
    }
}
