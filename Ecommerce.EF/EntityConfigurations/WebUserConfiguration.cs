using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                   .IsRequired();
            builder.HasIndex(h => h.Id)
                   .IsUnique();

            builder.HasOne(h => h.City)
                 .WithMany(w => w.Sellers)
                 .HasForeignKey(h => h.City_Id);
        }

        #endregion Methods
    }
}