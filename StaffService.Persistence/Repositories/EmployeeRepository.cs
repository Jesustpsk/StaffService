using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Domain.Models;
using StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IStaffServiceDbContext context) : base(context) { }

    public async Task<int> AddAsync(Employee employee)
    {
        var employeeId = await Connection.QuerySingleAsync<int>(SqlCommandsConstants.AddEmployee, employee);
        return employeeId;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await Connection.ExecuteAsync(SqlCommandsConstants.DeleteEmployee, new { id });
    }

    public async Task<int> UpdateAsync(UpdateEmployeeCommand updateEmployeeCommand)
    {
        return await Connection.ExecuteAsync(SqlCommandsConstants.UpdateEmployee, updateEmployeeCommand);
    }

    public async Task<IEnumerable<Employee>> GetListAsync()
    {
        var employees = await Connection.QueryAsync<Employee, Passport, Department, Employee>(
            SqlCommandsConstants.GetAllEmployeesWithDependencies,
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
        var employees = await Connection.QueryAsync<Employee, Passport, Department, Employee>(
            SqlCommandsConstants.GetEmployeeWithDependenciesById,
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