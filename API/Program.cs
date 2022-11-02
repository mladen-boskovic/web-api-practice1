using API.Core;
using API.Extensions;
using Bugsnag.AspNet.Core;
using DataAccess.EntityFramework;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
builder.Services.AddDbContext<WebApiPracticeContext>(options => options.UseSqlServer(appSettings.ConnectionString));
builder.Services.AddScoped(x => new JwtManager(x.GetService<WebApiPracticeContext>(), appSettings.JwtSettings));
builder.Services.AddJwt(appSettings.JwtSettings);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationUser();
builder.Services.AddUseCasesData();
builder.Services.AddRawSqlDbConnection(appSettings);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddBugsnag(x => { x.ApiKey = appSettings.BugsnagKey; });
builder.Services.AddScoped(x => RabbitHutch.CreateBus("host=localhost"));
builder.Services.AddSwaggerAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
