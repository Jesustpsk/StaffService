using System.Reflection;
using StaffService.Application.Interfaces;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffService.Persistence.Dapper;
using StaffService.Persistence.Repositories;
using StaffService.Persistence.Services;

namespace StaffService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("StaffServiceDbContext");
        if (string.IsNullOrEmpty(connectionString))
            throw new ApplicationException("Connection string not found");
        
        services.AddDatabase(connectionString);
        services.AddMigrations(connectionString);
        services.AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IStaffServiceDbContext, StaffServiceDbContext>(_ => 
            new StaffServiceDbContext(connectionString));
        
        return services;
    }
    
    private static IServiceCollection AddMigrations(this IServiceCollection services, string connectionString)
    {
        return services.AddFluentMigratorCore()
            .ConfigureRunner(x => x
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddHostedService<UpdateDatabaseService>();
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
        
        return services;
    }
}