using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Entities;
using System;

namespace Services.Infra.Map
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.DueDate)
                .IsRequired(true);

            builder.Property(x => x.Cost);
            builder.Property(x => x.BarCode);
            builder.Property(x => x.PaymentType);
            builder.Property(x => x.PaidDate);
            builder.Property(x => x.Link);
            builder.Property(x => x.Notes);

            builder.ToTable("Payment").HasKey(x => x.Id);


            builder.HasData(new Payment()
            {
                Id = Guid.Parse("b413cfc0-f53a-4765-9430-3912efcd79cb"),
                Description = "Rent",
                BarCode = "0000 1111 2222 3333 4444",
                Cost = "1.395,20",
                PaymentType = PaymentType.Juridico.ToString(),
                DueDate = "05/04/2020",
                PaidDate = "05/04/2020",
                Link = "https://www.portalunsoft.com.br/area-do-cliente/safira/",
                Notes = "Notes",

            });
        }
    }

}
