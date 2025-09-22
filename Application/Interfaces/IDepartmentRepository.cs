using Domain.Models;

namespace Application.Interfaces;

public interface IDepartmentRepository
{
    public Task<int> AddDepartment(Department department);
}