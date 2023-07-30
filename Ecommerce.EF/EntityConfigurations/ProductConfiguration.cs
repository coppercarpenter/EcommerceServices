using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            // WebUser_Id FK Required
            builder.HasOne(h => h.Seller)
                   .WithMany(w => w.Products)
                   .HasForeignKey(h => h.Seller_Id);

            builder.HasOne(h => h.Category)
                   .WithMany(w => w.Products)
                   .HasForeignKey(h => h.Category_Id);

            builder.HasOne(h => h.Currency)
                   .WithMany(w => w.Products)
                   .HasForeignKey(h => h.Currency_Id);

            builder.HasOne(h => h.UOM)
                   .WithMany(w => w.Products)
                   .HasForeignKey(h => h.Uom_Id);

            builder.Property(p => p.MaxPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.MinPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.FlatPrice).HasColumnType("decimal(18,2)");
        }

        #endregion Methods
    }
}