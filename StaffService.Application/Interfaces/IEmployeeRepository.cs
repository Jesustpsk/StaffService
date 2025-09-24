using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using StaffService.Domain.Models;

namespace StaffService.Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> AddAsync(Employee employee);
    public Task<int> DeleteAsync(int id);
    public Task<int> UpdateAsync(Employee employee);
    public Task<IEnumerable<Employee>> GetListAsync(GetEmployeesQuery filterParams);
    public Task<Employee?> GetAsync(int id);
}