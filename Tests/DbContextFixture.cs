using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Core;

namespace Tests
{
    public class DbContextFixture
    {
        public WebApiPracticeContext Context
        {
            get
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json")
                    .AddEnvironmentVariables()
                    .Build();
                var appSettings = config.GetSection("ApplicationSettings").Get<ApplicationSettings>();
                var optionsBuilder = new DbContextOptionsBuilder();
                var connectionString = appSettings.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
                var options = optionsBuilder.Options;
                return new WebApiPracticeContext(options);
            }
        }

        public WebApiPracticeContext InMemoryContext
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<WebApiPracticeContext>();
                optionsBuilder.UseInMemoryDatabase("WebApiPractice");
                var options = optionsBuilder.Options;
                var context = new WebApiPracticeContext(options);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.AddData();
                context.SaveChanges();
                return context;
            }
        }
    }
}
