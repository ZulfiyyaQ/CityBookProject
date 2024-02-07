using CityBookMVCOnionDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBookMVCOnionPersistence.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(u => u.Surname)
               .IsRequired()
               .HasMaxLength(25);
            builder.Property(u => u.Description)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(u => u.Face)
               .IsRequired();

            builder.Property(u => u.Tvit)
                .IsRequired();

            builder.Property(u => u.Link)
                .IsRequired();
        }
    }
    
}
