using StaffService.Application.Interfaces;
using Dapper;

namespace StaffService.Persistence.Repositories;

public abstract class BaseRepository<TEntity>
{
    protected readonly IStaffServiceDbContext Context;

    protected BaseRepository(IStaffServiceDbContext context)
    {
        Context = context;
    }

    protected async Task<T> QuerySingleAsync<T>(string sql, object param = null)
    {
        using var connection = await Context.CreateConnectionAsync();
        return await connection.QuerySingleAsync<T>(sql, param);
    }

    protected async Task<int> ExecuteAsync(string sql, object param = null)
    {
        using var connection = await Context.CreateConnectionAsync();
        return await connection.ExecuteAsync(sql, param);
    }
}