using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            builder.HasOne(h => h.Customer)
                   .WithMany(w => w.Orders)
                   .HasForeignKey(h => h.Customer_Id);
        }

        #endregion Methods
    }
}