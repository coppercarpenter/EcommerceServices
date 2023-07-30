using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();

            builder.HasOne(h => h.Country)
                   .WithMany(w => w.Cities)
                   .HasForeignKey(h => h.Country_Id)
                   .IsRequired();
        }

        #endregion Methods
    }
}