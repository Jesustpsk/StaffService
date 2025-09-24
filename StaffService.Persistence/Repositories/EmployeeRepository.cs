using System.Text;
using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using StaffService.Domain.Models;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IStaffServiceDbContext context) : base(context) { }

    public async Task<int> AddAsync(Employee employee)
        => await QuerySingleAsync<int>(SqlCommandsConstants.AddEmployee, employee);

    public async Task<int> DeleteAsync(int id)
        => await ExecuteAsync(SqlCommandsConstants.DeleteEmployee, new { id });
    
    public async Task<int> UpdateAsync(Employee employee)
        => await ExecuteAsync(SqlCommandsConstants.UpdateEmployee, employee);

    public async Task<IEnumerable<Employee>> GetListAsync(GetEmployeesQuery filterParams)
    {
        var baseQuery = SqlCommandsConstants.GetAllEmployeesWithDependencies;
        var sb = new StringBuilder();
        var parameters = new DynamicParameters();
        
        sb.Append(baseQuery);

        var conditions = new List<string>();

        if (filterParams.CompanyId != null)
        {
            conditions.Add("e.company_id = @CompanyId");
            parameters.Add("CompanyId", filterParams.CompanyId);
        }

        if (!string.IsNullOrEmpty(filterParams.DepartmentName))
        {
            conditions.Add("d.name = @DepartmentName");
            parameters.Add("DepartmentName", filterParams.DepartmentName);
        }
        
        if (conditions.Count != 0)
        {
            sb.Append(" WHERE ").Append(string.Join(" AND ", conditions));
        }
        
        using var connection = await Context.CreateConnectionAsync();
        
        var employees = await connection.QueryAsync<Employee, Passport, Department, Employee>(
            sb.ToString(),
            (employees, passports, departments) =>
            {
                employees.Passport = passports;
                employees.Department = departments;
                return employees;
            },
            parameters,
            splitOn: "p_id,d_id"
        );
        
        return employees;
    }

    public async Task<Employee?> GetAsync(int id)
    {
        using var connection = await Context.CreateConnectionAsync();
        
        var employees = await connection.QueryAsync<Employee, Passport, Department, Employee>(
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