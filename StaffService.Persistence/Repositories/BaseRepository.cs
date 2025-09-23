using System.Data;
using StaffService.Application.Interfaces;
using Dapper;

namespace StaffService.Persistence.Repositories;

public abstract class BaseRepository<TEntity>
{
    protected readonly IDbConnection Connection;

    protected BaseRepository(IStaffServiceDbContext context)
    {
        Connection = context.CreateConnection();
    }

    public virtual async Task<int> AddAsync(TEntity entity, string sql)
        => await Connection.QuerySingleAsync<int>(sql, entity);

    public virtual async Task<int> DeleteAsync(int id, string sql)
        => await Connection.ExecuteAsync(sql, new { id });
}