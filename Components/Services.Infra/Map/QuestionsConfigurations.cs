using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class QuestionsConfigurations : IEntityTypeConfiguration<Questions>
    {
        public void Configure(EntityTypeBuilder<Questions> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Question)
                .IsRequired(true);

            builder.Property(x => x.CorrectOption)
                .IsRequired(true);

            builder.Property(x => x.Option1);
            builder.Property(x => x.Option2);
            builder.Property(x => x.Option3);
            builder.Property(x => x.Option4);

            builder.ToTable("Questions").HasKey(x => x.Id);


            builder.HasData(new Questions()
            {
                Id = Guid.Parse("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                Question = "Quanto é 1 + 1",
                Option1 = "1",
                Option2 = "2",
                Option3 = "3",
                Option4 = "4",
                CorrectOption = "Option2",
            });
        }
    }

}
