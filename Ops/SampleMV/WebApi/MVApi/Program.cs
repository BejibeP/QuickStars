using QuickStars.MVApi.Extensions;

namespace QuickStars.MVApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Services

            // Load configuration from appsettings & environnment variable
            builder.Services.LoadConfiguration(builder.Configuration);

            builder.Services.ConfigureDatabase(builder.Configuration);

            builder.Services.ConfigureIdentity(builder.Configuration);

            builder.Services.ConfigureDependencyInjection();

            builder.Services.ConfigureSwagger(builder.Configuration);

            // Configure Logging
            builder.Logging.ConfigureLogging();

            // Build the app

            var app = builder.Build();

            // try to migrate the db once
            //app.Migrate();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //       - ConnectionStrings__DefaultConnection=Server=mv-sql;Database=MvDb;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True
            //}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            // deactive for test
            // app.UseHttpsRedirection();

            app.ConfigureCORS();
            
            app.MapControllers();

            app.Run();
        }
    }
}