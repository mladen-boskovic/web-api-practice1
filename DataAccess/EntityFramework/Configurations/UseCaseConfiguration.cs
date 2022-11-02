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
    public class UseCaseConfiguration : EntityConfiguration<UseCase>
    {
        protected override void ConfigureRules(EntityTypeBuilder<UseCase> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(uc => uc.UserUseCases).WithOne(uuc => uuc.UseCase).HasForeignKey(uuc => uuc.UseCaseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
