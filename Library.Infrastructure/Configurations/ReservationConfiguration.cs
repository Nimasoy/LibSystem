using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(e => e.BookId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.ReservationDate)
                .IsRequired();

            builder.Property(e => e.ExpiryDate)
                .IsRequired();

            builder.Property(e => e.IsFulfilled)
                .IsRequired()
                .HasDefaultValue(false);

            // Add indexes for common queries
            builder.HasIndex(e => new { e.BookId, e.UserId });
            builder.HasIndex(e => e.ExpiryDate);
            builder.HasIndex(e => e.IsFulfilled);

            // Configure relationships
            builder.HasOne(e => e.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(e => e.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
