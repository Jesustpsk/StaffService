namespace Persistence.Constants;

public static class SqlPathsConstants
{
    private const string RootPath = @"Dapper\Sql";

    private const string EmployeeModelPath = @"Employee";
    private const string DepartmentModelPath = @"Department";
    private const string PassportModelPath = @"Passport";
    
    public const string AddEmployee = $@"{RootPath}{EmployeeModelPath}\AddEmployee.sql";
    public const string DeleteEmployee = $@"{RootPath}{EmployeeModelPath}\DeleteEmployee.sql";
    public const string UpdateEmployee = $@"{RootPath}{EmployeeModelPath}\UpdateEmployee.sql";
    public const string GetEmployeeById = $@"{RootPath}{EmployeeModelPath}\GetEmployeeById.sql";
    public const string GetEmployeeWithNestedTablesById = $@"{RootPath}{EmployeeModelPath}\GetEmployeeWithNestedTablesById.sql";
    public const string GetAllEmployees = $@"{RootPath}{EmployeeModelPath}\GetEmployees.sql";
    public const string GetAllEmployeesWithNestedTables = $@"{RootPath}{EmployeeModelPath}\GetEmployeesWithNestedTables.sql";
    
    public const string AddDepartment = $@"{RootPath}{DepartmentModelPath}\AddDepartment.sql";
    
    public const string AddPassport = $@"{RootPath}{PassportModelPath}\AddPassport.sql";
}