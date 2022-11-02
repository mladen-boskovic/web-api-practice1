using DataAccess.EntityFramework;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class ContextExtensions
    {
        public static void AddData(this WebApiPracticeContext context)
        {
            context.Users.AddRange(new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Pera",
                    LastName = "Peric",
                    Username = "peraperic",
                    Email = "pera@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"),
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    FirstName = "Laza",
                    LastName = "Lazic",
                    Username = "lazalazic",
                    Email = "laza@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Sifra123!"),
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            });
        }
    }
}
