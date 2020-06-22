using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class EstimatedtimeTrackerConfigurations : IEntityTypeConfiguration<EstimatedtimeTracker>
    {
        public void Configure(EntityTypeBuilder<EstimatedtimeTracker> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Project)
                .IsRequired(true);

            builder.Property(x => x.Activity)
                .IsRequired(true);

            builder.Property(x => x.TimeStarted)
               .IsRequired(false);

            builder.Property(x => x.TimeEnded)
               .IsRequired(false);

            builder.Property(x => x.TimeSpent)
               .IsRequired(false);

            builder.Property(x => x.Owner)
              .IsRequired(true);

            builder.ToTable("EstimatedtimeTracker").HasKey(x => x.Id);

            builder.HasData(new EstimatedtimeTracker()
            {
                Id = Guid.NewGuid(),
                Owner = "Email@Email.com",
                Project = "Project",
                Activity = "Activity",
                TimeStarted = DateTime.Now.ToShortTimeString(),
                TimeEnded = DateTime.Now.AddHours(2).ToShortTimeString(),
                TimeSpent = DateTime.Now.ToShortTimeString() + DateTime.Now.AddHours(2).ToShortTimeString(),
                Date = DateTime.Now.ToShortDateString(),
            });
        }
    }

}
