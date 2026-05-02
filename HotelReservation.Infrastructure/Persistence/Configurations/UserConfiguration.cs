using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure  (EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nom).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Courriel).IsRequired().HasMaxLength(100);


            builder.HasIndex(u=> u.Courriel).IsUnique();

            builder.Property(u => u.Role).IsRequired().HasConversion<string>();
            builder.Property(u => u.EstActif).IsRequired().HasDefaultValue(true);
        }
    }
}
