using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class ArticleConfigurations : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true);

            builder.Property(x => x.Text)
                .IsRequired(true);

            builder.Property(x => x.Access)
               .IsRequired(true);

            builder.Property(x => x.UserEmail)
               .IsRequired(true);


            builder.ToTable("Article").HasKey(x => x.Id);

            builder.HasData(new Article()
            {
                Id = Guid.NewGuid(),
                UserEmail = "Email@Email.com",
                Description = "Article Description",
                Text = "Article Text",
                Access = ArticleAccess.Private,
            });
        }
    }

}
