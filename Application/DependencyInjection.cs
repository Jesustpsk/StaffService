using System.Reflection;
using Application.Common.Behaviors;
using Application.Common.Mapping;
using Application.CQRS.Employees.Queries.GetEmployee;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(GetEmployeeQueryHandler).Assembly));
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}