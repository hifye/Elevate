using Application.Features.Auth.Commands.Login;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(opt => opt.AddMaps(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        
        return services;
    }
}