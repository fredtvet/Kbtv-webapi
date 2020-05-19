using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class ReportTypeConfiguration : BaseEntityConfiguration<ReportType>
    {
        public override void Configure(EntityTypeBuilder<ReportType> builder)
        {
            base.Configure(builder);

            builder.Property(mt => mt.Name).IsRequired().HasMaxLength(45);
        }
    }
}
