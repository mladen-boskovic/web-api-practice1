using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Implementation.HanfireJobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Core
{
    public static class StartupManager
    {
        public static ApplicationSettings BindConfiguration(string[] args)
        {
            var appSettings = new ApplicationSettings();

            IHost host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;

                configuration
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true);

                IConfigurationRoot configurationRoot = configuration.Build();

                configurationRoot.GetSection(nameof(ApplicationSettings)).Bind(appSettings);
            }).Build();

            return appSettings;
        }

        public static void AddHangfire(IServiceCollection services, ApplicationSettings appSettings)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(appSettings.HangfireConnection, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();
        }

        public static void AddJobs()
        {
            RecurringJob.AddOrUpdate<UserJobs>("Get All Users", x => x.GetAllUsers(), Cron.MinuteInterval(1));
        }
    }
}
