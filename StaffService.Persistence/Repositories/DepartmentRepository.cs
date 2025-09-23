using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Domain.Models;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(IStaffServiceDbContext context) : base(context) { }
    
    public async Task<int> AddAsync(Department department)
    {
        var query = SqlCommandsConstants.AddDepartment;

        var departmentId = await Connection.QuerySingleAsync<int>(query, department);
        return departmentId;
    }
}