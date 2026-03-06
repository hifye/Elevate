using System.Data;
using Application.Contracts.Repositories.Auth;
using Application.Contracts.Repositories.Catalog;
using Application.Contracts.Repositories.Learning;
using Application.Contracts.UnitOfWork;
using Infrastructure.Persistance;
using Infrastructure.Repositories.Auth;
using Infrastructure.Repositories.Catalog;
using Infrastructure.Repositories.Learning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        
        services.AddScoped<IDbConnection>(_ =>
            new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}