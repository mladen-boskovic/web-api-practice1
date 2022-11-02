using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UseCase>().HasData(new List<UseCase>
            {
                new UseCase
                {
                    Id = 1,
                    Name = "Register User using Entity Framework"
                },
                new UseCase
                {
                    Id = 2,
                    Name = "Register User using Raw Sql"
                },
                new UseCase
                {
                    Id = 3,
                    Name = "Get User using Entity Framework"
                }
            });
        }
    }
}
