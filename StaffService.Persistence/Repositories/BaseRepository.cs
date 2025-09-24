using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Application.Common.Exceptions;

namespace StaffService.Persistence.Repositories;

public abstract class BaseRepository<TEntity>
{
    protected readonly IStaffServiceDbContext Context;

    protected BaseRepository(IStaffServiceDbContext context)
    {
        Context = context;
    }

    protected async Task<T> QuerySingleAsync<T>(string sql, object param)
    {
        if (param == null)
            throw new ArgumentNullException(nameof(param));
        using var connection = await Context.CreateConnectionAsync();
        return await connection.QuerySingleAsync<T>(sql, param);
    }

    protected async Task<int> ExecuteAsync(string sql, object param)
    {
        using var connection = await Context.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, param);

        if (affectedRows > 0)
            return affectedRows;

        var id = param switch
        {
            int p => p,
            _ => param.GetType().GetProperty("Id")?.GetValue(param)
        };

        throw new NotFoundException(typeof(TEntity).Name, id);
    }
}