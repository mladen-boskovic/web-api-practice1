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
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            ConfigureRules(builder);
        }

        protected abstract void ConfigureRules(EntityTypeBuilder<T> builder);
    }
}
