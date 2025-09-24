using StaffService.Domain.Models;

namespace StaffService.Application.Interfaces;

public interface IDepartmentRepository
{
    public Task<int> AddAsync(Department department);
    public Task<int> UpdateAsync(Department employee);
}