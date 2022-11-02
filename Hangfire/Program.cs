using DataAccess.EntityFramework;
using Hangfire;
using Hangfire.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



var appSettings = StartupManager.BindConfiguration(args);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebApiPracticeContext>(options => options.UseSqlServer(appSettings.ConnectionString));

StartupManager.AddHangfire(builder.Services, appSettings);

var app = builder.Build();

app.UseHangfireDashboard();

StartupManager.AddJobs();

app.Run();