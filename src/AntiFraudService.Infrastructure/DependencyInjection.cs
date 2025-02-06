using AntiFraudService.Application.Common.Interfaces;
using AntiFraudService.Infrastructure.AntiFrauds;
using AntiFraudService.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AntiFraudService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            string? connectionString = configuration.GetConnectionString("AntiFraudConnectionString");

            services.AddDbContext<AntiFraudDbContext>(options =>

                options.UseNpgsql(connectionString, options =>
                {
                }));

            services.AddScoped<IAntiFraudRepository, AntiFraudRepository>();

            return services;
        }
    }
}
