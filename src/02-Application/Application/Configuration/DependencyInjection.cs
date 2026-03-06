using Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(opt => opt.AddMaps(typeof(DependencyInjection).Assembly));
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}