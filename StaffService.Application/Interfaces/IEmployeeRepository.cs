using System.Data;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;

namespace StaffService.Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> AddAsync(Employee employee);
    public Task<int> DeleteAsync(int id);
    public Task<int> UpdateAsync(UpdateEmployeeCommand updateEmployeeCommand);
    public Task<IEnumerable<Employee>> GetListAsync();
    public Task<Employee?> GetAsync(int id);
}