using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            builder.HasOne(h => h.Product)
                .WithMany(w => w.ProductImages)
                .HasForeignKey(h => h.Product_Id);
        }

        #endregion Methods
    }
}