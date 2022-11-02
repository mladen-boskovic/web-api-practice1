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
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderLines).WithOne(ol => ol.Order).HasForeignKey(ol => ol.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
