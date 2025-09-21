using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Dapper;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        return services;
    }
}