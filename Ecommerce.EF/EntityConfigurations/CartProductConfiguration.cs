using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            builder.HasOne(h => h.Customer)
                   .WithMany(w => w.CartProducts)
                   .HasForeignKey(h => h.Customer_Id);

            builder.HasOne(h => h.Product)
                   .WithMany(w => w.CartProducts)
                   .HasForeignKey(h => h.Product_Id);
        }

        #endregion Methods
    }
}