using StaffService.Application.CQRS.Employees.Queries.GetEmployee;
using Serilog;

namespace StaffService.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options => 
            options.RegisterServicesFromAssembly(typeof(GetEmployeeQueryHandler).Assembly));
        
        services.AddSerilog((_, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(configuration));
        return services;
    }
}