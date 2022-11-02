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
    public class ShopConfiguration : EntityConfiguration<Shop>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Shop> builder)
        {
            builder.HasMany(s => s.Orders).WithOne(o => o.Shop).HasForeignKey(o => o.ShopId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(s => s.Products).WithOne(p => p.Shop).HasForeignKey(p => p.ShopId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
