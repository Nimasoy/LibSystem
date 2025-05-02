using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired();

            builder.OwnsOne(c => c.Name, name =>
            {
                name.Property(cn => cn.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasConversion(
                        v => v.Value, // Convert CategoryName to string for database
                        v => new Library.Domain.ValueObjects.Category.CategoryName(v)); // Convert string from database to CategoryName
            });
        }
    }
}
