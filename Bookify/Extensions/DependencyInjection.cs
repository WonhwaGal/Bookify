using Bookify.Models.Services.Impl;
using Bookify.Models.Services;
using System.Runtime.CompilerServices;
using Bookify.Options;
using Microsoft.AspNetCore.Mvc;
using Bookify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Bookify.Infrastructure.Services;
using Bookify.Infrastructure.Services.Impl;
using Dapper;
using Bookify.Infrastructure.Data;

namespace Bookify.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // will live as long as controller lives
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.Configure<MyTestOptions1>(settings =>
                configuration.GetSection("MyServices:MyService_1").Bind(settings));

            services.Configure<MyTestOptions2>(settings =>
            {
                configuration.GetSection("MyServices:MyService_2").Bind(settings);
                settings.ConnectionString = configuration.GetConnectionString("Bookify-Database");
            });

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Bookify-Database") ?? 
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(connectionString));

            //поддержка типа DateOnly в Dapper
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            return services;
        }
    }
}
