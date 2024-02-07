using CityBookMVCOnionDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBookMVCOnionPersistence.Configurations
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(25);
            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
   
}
