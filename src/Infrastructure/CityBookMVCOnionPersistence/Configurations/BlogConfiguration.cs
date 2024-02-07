using CityBookMVCOnionDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBookMVCOnionPersistence.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(u => u.Text)
                .IsRequired()
                .HasMaxLength(1500);
        }
    }
    
}
