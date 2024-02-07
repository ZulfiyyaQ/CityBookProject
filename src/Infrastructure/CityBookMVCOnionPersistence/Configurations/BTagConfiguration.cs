using CityBookMVCOnionDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBookMVCOnionPersistence.Configurations
{
    internal class BTagConfiguration : IEntityTypeConfiguration<BTag>
    {
        public void Configure(EntityTypeBuilder<BTag> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
    
}
