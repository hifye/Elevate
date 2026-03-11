using System.Data;
using System.Text;
using Application.Abstraction.Persistance.Repositories.Auth;
using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Abstraction.Persistance.Repositories.Learning;
using Application.Abstraction.Queries;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Infrastructure.Data.Queries;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories.Auth;
using Infrastructure.Persistance.Repositories.Catalog;
using Infrastructure.Persistance.Repositories.Learning;
using Infrastructure.Persistance.UnitOfWork;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
        services.AddScoped<IModuleQueries, ModuleQueries>();
        services.AddScoped<ICourseQueries, CourseQueries>();
        services.AddScoped<ILessonQueries, LessonQueries>();
        services.AddScoped<IEnrollmentQueries, EnrollmentQueries>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddHttpContextAccessor();

        services.AddScoped<IDbConnection>(_ =>
            new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

        return services;
    }
}