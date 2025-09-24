using StaffService.Application.Interfaces;
using StaffService.Domain.Models;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(IStaffServiceDbContext context) : base(context) { }
    
    public async Task<int> AddAsync(Department department)
        => await QuerySingleAsync<int>(SqlCommandsConstants.AddDepartment, department);

    public async Task<int> UpdateAsync(Department department)
        => await ExecuteAsync(SqlCommandsConstants.UpdateDepartment, department);
}