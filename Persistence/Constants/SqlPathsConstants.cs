namespace Persistence.Constants;

public static class SqlPathsConstants
{
    private const string RootPath = @"Dapper\Sql";

    private const string EmploeeModelPath = @"Employee";
    private const string DepartmentModelPath = @"Employee";
    private const string PassportModelPath = @"Employee";
    
    public const string AddEmployee = $@"{RootPath}{EmploeeModelPath}\AddEmployee.sql";
    public const string DeleteEmployee = $@"{RootPath}{EmploeeModelPath}\DeleteEmployee.sql";
    public const string UpdateEmployee = $@"{RootPath}{EmploeeModelPath}\UpdateEmployee.sql";
    public const string GetEmployeeById = $@"{RootPath}{EmploeeModelPath}\GetEmployeeById.sql";
    public const string GetEmployeeWithNestedTablesById = $@"{RootPath}{EmploeeModelPath}\GetEmployeeWithNestedTablesById.sql";
    public const string GetAllEmployees = $@"{RootPath}{EmploeeModelPath}\GetEmployees.sql";
    public const string GetAllEmployeesWithNestedTables = $@"{RootPath}{EmploeeModelPath}\GetEmployeesWithNestedTables.sql";
    
    public const string AddDepartment = $@"{RootPath}{DepartmentModelPath}\AddDepartment.sql";
    
    public const string AddPassport = $@"{RootPath}{PassportModelPath}\AddPassport.sql";
}