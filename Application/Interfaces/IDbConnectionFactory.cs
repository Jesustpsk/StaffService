using System.Data;

namespace Application.Interfaces;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}