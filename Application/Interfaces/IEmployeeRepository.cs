using Domain.Models;
using MediatR;

namespace Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> AddEmployeeAsync(Employee employee);
    public Task<Unit> DeleteEmployeeAsync(int id);
    public Task<Unit> UpdateEmployeeAsync(int id);
    public Task<IEnumerable<Employee>> GetEmployeesAsync(Dictionary<string, object?>? filters, string sortBy, bool ascending, int? page, int? pageSize);
    public Task<Employee?> GetEmployeeAsync(int id);
}