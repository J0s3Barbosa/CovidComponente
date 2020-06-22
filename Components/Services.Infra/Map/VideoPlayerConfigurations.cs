using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class VideoPlayerConfigurations : IEntityTypeConfiguration<VideoPlayer>
    {
        public void Configure(EntityTypeBuilder<VideoPlayer> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true);

            builder.Property(x => x.Link)
                .IsRequired(true);

            builder.Property(x => x.UserEmail)
               .IsRequired(true);

            builder.ToTable("VideoPlayer").HasKey(x => x.Id);

            builder.HasData(new VideoPlayer()
            {
                Id = Guid.NewGuid(),
                UserEmail = "Email@Email.com",
                Description = "Description",
                Link = "https://www.youtube.com/watch?v=4MkuId9X-hk",
            });
        }
    }

}
