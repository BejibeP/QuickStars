using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Business.Services;
using QuickStars.MVApi.Domain;
using QuickStars.MVApi.Domain.Configuration;
using QuickStars.MVApi.Domain.Data.Interceptors;
using QuickStars.MVApi.Domain.Interfaces;
using QuickStars.MVApi.Domain.Repositories;
using QuickStars.MVApi.Workers;
using System.Text;

namespace QuickStars.MVApi.Extensions
{
    public static class ServiceExtensions
    {

        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var appSection = configurationManager.GetSection(AppSettings.App);
            services.Configure<AppSettings>(appSection);

            var apiSection = configurationManager.GetSection(ExternalApiSettings.ExternalAPIs);
            services.Configure<ExternalApiSettings>(apiSection);

            var databaseSection = configurationManager.GetSection(DatabaseSettings.DatabaseConnections);
            services.Configure<DatabaseSettings>(databaseSection);

            var messagingSection = configurationManager.GetSection(MessagingSettings.Messaging);
            services.Configure<MessagingSettings>(messagingSection);

            var loggingSection = configurationManager.GetSection(LoggingSettings.Logging);
            services.Configure<LoggingSettings>(loggingSection);

        }

        public static void ConfigureLogging(this ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "HH:mm:ss ";
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // add repositories
            services.AddScoped<ILogMessageRepository, LogMessageRepository>();

            // add services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ILogMessageService, LogMessageService>();

            services.AddScoped<IConfigurationService, ConfigurationService>();

            services.AddSingleton<IRndLoggerService, RndLoggerService>();
            services.AddHostedService<MVWorker>();

            services.AddControllers();
        }

        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            // add interceptors into DI
            services.AddSingleton<SaveInterceptor>();

            // configure DatabaseContext with serviceProvider and optionBuilder
            services.AddDbContext<DatabaseContext>((provider, options) =>
            {
                options.AddInterceptors(provider.GetRequiredService<SaveInterceptor>());
                //options.UseInMemoryDatabase("MVApiDB");
                options.UseSqlServer("Server=localhost,1467;Database=MV7;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            });
            /*
            
            public string BuildConnectionString()
            {
                // "Server=localhost,1467;Database=MvDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True;"
                return "Server={Host},{Port}Database";
            }
            */
        }

        public static void Migrate(this IApplicationBuilder app)
        {
            using (var services = app.ApplicationServices.CreateScope())
            {
                var dbService = services.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (dbService == null) return;
                dbService.Database.Migrate();
                // Takes all of our migrations files and apply them against the database in case they are not implemented
            }
        }

        public static void ConfigureIdentity(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            //var identitySettings = configurationManager.GetSection("Identity").Get<IdentitySettings>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tokeneeealfkzld1122")),//identitySettings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = "",//identitySettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = "",//identitySettings.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

        }

        public static void ConfigureCORS(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddEndpointsApiExplorer();

            //configuration de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MaViCS.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Ajouter le token ainsi : \"Bearer xxxx\" où xxxx est votre token d'authentification",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

    }
}