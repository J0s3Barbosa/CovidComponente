using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class healthCheckConfigurations : IEntityTypeConfiguration<HealthCheck>
    {
        public void Configure(EntityTypeBuilder<HealthCheck> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.AppName)
                .IsRequired(true);

            builder.Property(x => x.TimeAccessed)
                .IsRequired(true);

            builder.ToTable("HealthCheck").HasKey(x => x.Id);

            builder.HasData(new HealthCheck()
            {
                Id = Guid.NewGuid(),
                AppName = "API",
                TimeAccessed = DateTime.Now.ToString(),
            });
        }
    }

}
