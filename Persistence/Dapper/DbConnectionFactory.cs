using System.Data;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Dapper;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;
    
    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        
        await connection.OpenAsync();
        return connection;
    }
}