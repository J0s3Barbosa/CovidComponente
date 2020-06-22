using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.FirstName)
                .IsRequired(true);

            builder.Property(x => x.LastName)
            .IsRequired(true);

            builder.Property(x => x.Phone)
            .IsRequired(true);

            builder.Property(x => x.Email)
            .IsRequired(true);

            builder.Property(x => x.Password)
                .IsRequired(true);

            builder.ToTable("User").HasKey(x => x.Id);


            builder.HasData(new User()
            {
                Id = Guid.Parse("b413cfc0-f53a-4765-9430-3912efcd79c5"),
                FirstName = "FirstName",
                LastName = "LastName",
                Phone = "99999999999",
                Email = "Email@Email.com",
                Password = "Password",
            });
        }
    }

}
