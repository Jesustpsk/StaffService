using System.Data;

namespace StaffService.Application.Interfaces;

public interface IStaffServiceDbContext
{
    public Task<IDbConnection> CreateConnectionAsync();
}