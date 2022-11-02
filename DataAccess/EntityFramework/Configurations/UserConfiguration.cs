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
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureRules(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany(u => u.UserUseCases).WithOne(uuc => uuc.User).HasForeignKey(uuc => uuc.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Orders).WithOne(o => o.User).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.Carts).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
