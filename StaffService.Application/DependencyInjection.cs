using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StaffService.Application.Common.Behaviors;
using StaffService.Application.Common.Mapping;
using StaffService.Application.CQRS.Employees.Queries.GetEmployee;

namespace StaffService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(GetEmployeeQueryHandler).Assembly));
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddMapper();
        return services;
    }
    
    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        });
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        
        return services;
    }
}