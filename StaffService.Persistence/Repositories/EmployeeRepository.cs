using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Common.Exceptions;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using StaffService.Persistence.Constants;
using StaffService.Persistence.Helpers;

namespace StaffService.Persistence.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IStaffServiceDbContext context) : base(context) { }

    public async Task<int> AddAsync(Employee employee)
    {
        var query = SqlCommandsConstants.AddEmployee;

        var employeeId = await Connection.QuerySingleAsync<int>(query, employee);
        return employeeId;
    }

    public async Task<Unit> DeleteAsync(int id)
    {
        var query = SqlCommandsConstants.DeleteEmployee;

        await Connection.ExecuteAsync(query, id);
        return Unit.Value;
    }

    public async Task<Unit> UpdateAsync(int id)
    {
        var query = SqlCommandsConstants.UpdateEmployee;

        await Connection.ExecuteAsync(query, id);
        return Unit.Value;
    }

    public async Task<IEnumerable<Employee>> GetListAsync(GetEmployeesQuery queryParams)
    {
        var query = SqlCommandsConstants.GetAllEmployeesWithDependencies;
        
        var employees = await Connection.QueryAsync<Employee, Passport, Department, Employee>(
            query,
            (employees, passports, departments) =>
            {
                employees.Passport = passports;
                employees.Department = departments;
                return employees;
            },
            splitOn: "p_id,d_id"
        );
        
        return employees;
    }

    public async Task<Employee?> GetAsync(int id)
    {
        var query = SqlCommandsConstants.GetEmployeeWithDependenciesById;
        
        var employees = await Connection.QueryAsync<Employee, Passport, Department, Employee>(
            query,
            (employees, passports, departments) =>
            {
                employees.Passport = passports;
                employees.Department = departments;
                return employees;
            },
            new { id },
            splitOn: "p_id,d_id"
        );
        
        return employees.SingleOrDefault();
    }
}