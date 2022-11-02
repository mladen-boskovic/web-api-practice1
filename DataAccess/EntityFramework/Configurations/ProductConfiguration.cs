using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Configurations
{
    public class ProductConfiguration : EntityConfiguration<Product>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Price).HasPrecision(10, 4);

            builder.HasMany(p => p.OrderLines).WithOne(ol => ol.Product).HasForeignKey(ol => ol.ProductId).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(p => p.Carts).WithOne(c => c.Product).HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
