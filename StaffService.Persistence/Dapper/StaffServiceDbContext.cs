using System.Data;
using StaffService.Application.Interfaces;
using Npgsql;

namespace StaffService.Persistence.Dapper;

public class StaffServiceDbContext : IStaffServiceDbContext, IDisposable
{
    private readonly string _connectionString;
    private IDbConnection _connection;

    public StaffServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public void Dispose()
    {
        if (_connection is { State: ConnectionState.Open })
        {
            _connection.Close();
            _connection.Dispose();
        } 
    }
}