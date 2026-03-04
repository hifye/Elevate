using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infraestructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnection>(_ =>
            new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}