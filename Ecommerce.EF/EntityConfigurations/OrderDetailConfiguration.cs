using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            builder.HasOne(h => h.Order)
                   .WithMany(w => w.OrderDetails)
                   .HasForeignKey(h => h.Order_Id);

            builder.HasOne(h => h.Currency)
                   .WithMany(w => w.OrderDetails)
                   .HasForeignKey(h => h.Currency_Id);

         
            builder.HasOne(h => h.UnitOfMeasure)
                    .WithMany(w => w.OrderDetails)
                    .HasForeignKey(h => h.UnitOfMeasure_Id);

            builder.Property(p => p.PerUnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }

        #endregion Methods
    }
}