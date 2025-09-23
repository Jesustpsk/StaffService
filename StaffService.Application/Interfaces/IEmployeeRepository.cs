using System.Data;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;

namespace StaffService.Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> AddAsync(Employee employee);
    public Task<Unit> DeleteAsync(int id);
    public Task<Unit> UpdateAsync(int id);
    public Task<IEnumerable<Employee>> GetListAsync(GetEmployeesQuery queryParams);
    public Task<Employee?> GetAsync(int id);
}