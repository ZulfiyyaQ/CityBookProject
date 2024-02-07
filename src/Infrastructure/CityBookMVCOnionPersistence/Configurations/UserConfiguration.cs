using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityBookMVCOnionDomain.Entities;

namespace CityBookMVCOnionPersistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Email)
                .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(25);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.About)
                .IsRequired()
                .HasMaxLength(1500);

            builder.Property(u => u.Face)
                .IsRequired();

            builder.Property(u => u.Tvit)
                .IsRequired();

            builder.Property(u => u.Inst)
                .IsRequired();

            builder.Property(u => u.Link)
                .IsRequired();
            builder.Property(u => u.WebSite)
                .IsRequired();
        }
    }
    
}
