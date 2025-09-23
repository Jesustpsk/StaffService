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

    public IDbConnection CreateConnection()
    {
        if (_connection is not { State: ConnectionState.Open })
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }
        
        return _connection;
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