using Application.CQRS.Employee.Queries;
using MediatR;

namespace Application.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> AddEmployee();
    public Task<Unit> DeleteEmployee(int id);
    public Task<Unit> UpdateEmployee(int id);
    public Task<EmployeePagedList> GetEmployees();
    public Task<EmployeeVm> GetEmployee(int id);
}