using System.Data;
using Application.Interfaces;
using Dapper;
using Domain.Models;
using MediatR;
using Persistence.Constants;
using Persistence.Dapper;

namespace Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly StaffServiceDbContext _context;
    
    public EmployeeRepository(StaffServiceDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddEmployeeAsync(Employee employee)
    {
        using var connection = _context.CreateConnection();
        var query = await File.ReadAllTextAsync(SqlPathsConstants.AddEmployee);

        var employeeId = await connection.ExecuteAsync(query, employee);
        return employeeId;
    }

    public async Task<Unit> DeleteEmployeeAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = await File.ReadAllTextAsync(SqlPathsConstants.DeleteEmployee);

        await connection.ExecuteAsync(query, id);
        return Unit.Value;
    }

    public async Task<Unit> UpdateEmployeeAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = await File.ReadAllTextAsync(SqlPathsConstants.UpdateEmployee);

        await connection.ExecuteAsync(query, id);
        return Unit.Value;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Dictionary<string, object?>? filters = null,
        string sortBy = "id", bool ascending = true, int? page = null, int? pageSize = null)
    {
        var sql = await File.ReadAllTextAsync(SqlPathsConstants.GetAllEmployeesWithNestedTables);
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(sql);

        // Динамические фильтры
        if (filters != null)
        {
            foreach (var filter in filters)
            {
                if (filter.Value != null)
                {
                    builder.Where($"{filter.Key} = @{filter.Key}", new { filter.Value });
                }
            }
        }

        // Сортировка
        if (!string.IsNullOrEmpty(sortBy))
        {
            builder.OrderBy($"{sortBy} {(ascending ? "ASC" : "DESC")}");
        }

        var parameters = new DynamicParameters();
        if (filters != null)
        {
            foreach (var kv in filters)
            {
                parameters.Add(kv.Key, kv.Value);
            }
        }

        // Пагинация
        if (page.HasValue && pageSize.HasValue)
        {
            var offset = (page.Value - 1) * pageSize.Value;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize.Value);

            if (!sql.Contains("OFFSET"))
            {
                template = builder.AddTemplate(sql + " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            }
        }

        using var connection = _context.CreateConnection();
        var employees = await connection.QueryAsync<Employee, Department, Passport, Employee>(
            template.RawSql,
            (employee, department, passport) =>
            {
                employee.Department = department;
                employee.Passport = passport;
                return employee;
            },
            param: parameters,
            splitOn: "id,id"
        );
        
        return employees;
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Employee>(SqlPathsConstants.GetEmployeeWithNestedTablesById, id);
    }
}