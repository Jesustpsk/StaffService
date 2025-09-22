using Application.Interfaces;
using Dapper;
using Domain.Models;
using Persistence.Constants;
using Persistence.Dapper;

namespace Persistence.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly StaffServiceDbContext _context;

    public DepartmentRepository(StaffServiceDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddDepartment(Department department)
    {
        using var connection = _context.CreateConnection();
        var query = await File.ReadAllTextAsync(SqlPathsConstants.AddDepartment);

        var departmentId = await connection.ExecuteAsync(query, department);
        return departmentId;
    }
}