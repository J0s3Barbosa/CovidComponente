using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class TodoConfigurations : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true);

            builder.Property(x => x.Details)
                .IsRequired(false);

            builder.Property(x => x.TimeStarted)
               .IsRequired(false);

            builder.Property(x => x.TimeFinished)
               .IsRequired(false);

            builder.Property(x => x.UserEmail)
               .IsRequired(true);

            builder.Property(x => x.TaskComplete)
              .IsRequired(true);

            builder.ToTable("Todo").HasKey(x => x.Id);

            builder.HasData(new Todo()
            {
                Id = Guid.NewGuid(),
                UserEmail = "Email@Email.com",
                Description = "Description",
                Details = "Details",
                TimeStarted = DateTime.Now.ToString(),
                TimeFinished = DateTime.Now.AddHours(2).ToString(),
                TaskComplete = false,
            });
        }
    }

}
