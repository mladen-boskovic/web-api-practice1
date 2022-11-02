using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Configurations
{
    public class CartConfiguration : EntityConfiguration<Cart>
    {
        protected override void ConfigureRules(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(x => x.UnitPrice).HasPrecision(10, 4);
        }
    }
}
