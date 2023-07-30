using Ecommerce.DTO.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.EF.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        #region Methods

        public void Configure(EntityTypeBuilder<User> builder)
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