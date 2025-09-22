using System.Data;
using Npgsql;

namespace Persistence.Dapper;

public class StaffServiceDbContext : IDisposable
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