using API.Core;
using Application.Loggers;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Domain;
using Implementation.Loggers.ExceptionLoggers;
using Implementation.Loggers.UseCaseLoggers;
using Implementation.UseCases;
using Implementation.UseCases.Commands.EntityFramework;
using Implementation.UseCases.Commands.RawSql;
using Implementation.UseCases.Queries.EntityFramework;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace API.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCasesData(this IServiceCollection services)
        {
            services.AddScoped<UseCaseHandler>();

            services.AddScoped<IRegisterUserCommand, EfRegisterUserCommand>();
            //services.AddScoped<IRegisterUserCommand, RawSqlRegisterUserCommand>();

            services.AddScoped<IGetUserQuery, EfGetUserQuery>();

            services.AddScoped<RegisterUserValidator>();

            services.AddSingleton<IUseCaseLogger, ConsoleUseCaseLogger>();

            services.AddScoped<IExceptionLogger, BugsnagExceptionLogger>();
            //services.AddSingleton<IExceptionLogger, ConsoleExceptionLogger>();
        }

        public static void AddJwt(this IServiceCollection services, JwtSettings settings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonymousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    UseCaseIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("UserUseCases").Value)
                };

                return actor;
            });
        }

        public static void AddRawSqlDbConnection(this IServiceCollection services, ApplicationSettings appSettings)
        {
            services.AddScoped<IDbConnection>(x =>
            {
                var sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = appSettings.ConnectionString;
                return sqlConnection;
            });
        }

        public static void AddSwaggerAuthorization(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "web-api-practice1 API"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Authorization Header using Bearer scheme. Enter 'Bearer' [space] [token] \r\n\r\n Example: 'Bearer abc123'",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            },
                            Scheme = "oauth2"
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
