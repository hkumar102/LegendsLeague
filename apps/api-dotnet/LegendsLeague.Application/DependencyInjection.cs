using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using LegendsLeague.Application.Common.Behaviors;

namespace LegendsLeague.Application;

/// <summary>
/// Registers Application-layer services: MediatR, FluentValidation, AutoMapper, and pipeline behaviors.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds MediatR handlers, validators, AutoMapper profiles, and pipeline behaviors for the Application layer.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR: discover all IRequestHandler<> in this assembly (Series/Teams/Players/…)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // FluentValidation: discover all validators in this assembly
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        // AutoMapper: discover all Profiles in this assembly (e.g., MappingProfile)
        services.AddAutoMapper(assembly);

        // Pipeline: validation behavior runs before handlers
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
