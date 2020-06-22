using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class UserPointsConfigurations : IEntityTypeConfiguration<UserPoints>
    {
        public void Configure(EntityTypeBuilder<UserPoints> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Email)
                .IsRequired(true);

            builder.Property(x => x.Points)
                .IsRequired(true);

            builder.ToTable("UserPoints").HasKey(x => x.Id);


            builder.HasData(new UserPoints()
            {
                Id = Guid.Parse("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                Email = "Email@Email.com",
                Points = 0,
            });
        }
    }

}
