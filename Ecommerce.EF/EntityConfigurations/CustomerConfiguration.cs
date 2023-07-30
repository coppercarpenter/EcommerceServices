using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                   .IsRequired();
            builder.HasIndex(h => h.Id)
                   .IsUnique();

         }

        #endregion Methods
    }
}