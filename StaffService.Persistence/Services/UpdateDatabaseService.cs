using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace StaffService.Persistence.Services;

public class UpdateDatabaseService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateDatabaseService> _logger;
    private readonly string _connectionString;
    
    public UpdateDatabaseService(IServiceProvider serviceProvider, ILogger<UpdateDatabaseService> logger, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _connectionString = configuration.GetConnectionString("StaffServiceDbContext")!;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        EnsureDatabaseExists(_connectionString);
        
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        
        if (runner.HasMigrationsToApplyUp())
            _logger.LogInformation("Применение миграций");
        else
            _logger.LogInformation("Новых миграций нет");

        runner.MigrateUp();
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    private static void EnsureDatabaseExists(string connectionString)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;
        connectionStringBuilder.Database = "postgres"; // Подключаемся к системной БД

        using var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
        var exists = cmd.ExecuteScalar() != null;

        if (!exists)
        {
            cmd.CommandText = $"CREATE DATABASE \"{databaseName}\"";
            cmd.ExecuteNonQuery();
        }
    }
}
