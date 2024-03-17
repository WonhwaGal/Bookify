
using Bookify.Extensions;
using Bookify.Models.Services;
using Bookify.Models.Services.Impl;
using Bookify.Options;
using Bookify.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Bookify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddRepositories();

            builder.Services.AddScoped<PricingService>();
            builder.Services.AddScoped<ApartmentSettingService>();

            builder.Services.AddOptions(builder.Configuration);
            builder.Services.AddPersistence(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                //support for DateOnly in Swagger
                options.UseDateOnlyTimeOnlyStringConverters();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bookify API",
                    Description = "API взаимодействия с сервисом бронирования аппартаментов",
                    Contact = new OpenApiContact
                    {
                        Name = "Контактная информация",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Сведения о лицензии",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "BookifyAPI.xml");
                options.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyMigrations();
                //app.SeedData();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
