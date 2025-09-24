namespace StaffService.Persistence.Constants;

public static class SqlCommandsConstants
{
    #region Employee

    public const string AddEmployee =
        @"INSERT INTO employees (name, surname, phone, company_id, passport_id, department_id)
          VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId)
          RETURNING id;";

    public const string DeleteEmployee = @"DELETE FROM employees WHERE id = @id;";

    public const string UpdateEmployee = @"UPDATE employees 
                                            SET 
                                                name = COALESCE(@name, name),
                                                surname = COALESCE(@surname, surname),
                                                phone = COALESCE(@phone, phone),
                                                company_id = COALESCE(@CompanyId, company_id)
                                            WHERE id = @id;";

    public const string GetEmployeeById = @"SELECT * FROM employees WHERE id = @id;";

    public const string GetEmployeeWithDependenciesById = @"SELECT   e.id  
                                                                    ,e.name
                                                                    ,e.surname
                                                                    ,e.phone
                                                                    ,e.company_id
                                                                    ,p.id as p_id
                                                                    ,p.type
                                                                    ,p.number
                                                                    ,d.id as d_id
                                                                    ,d.name
                                                                    ,d.phone
                                                                FROM employees e
                                                                JOIN departments d ON d.id = e.department_id
                                                                JOIN passports p ON p.id = e.passport_id
                                                            WHERE e.id = @id";

    public const string GetAllEmployees = $@"SELECT * FROM employees";
    
    public const string GetAllEmployeesWithDependencies = $@"SELECT   e.id  
                                                                    ,e.name
                                                                    ,e.surname
                                                                    ,e.phone
                                                                    ,e.company_id AS CompanyId
                                                                    ,p.id as p_id
                                                                    ,p.type
                                                                    ,p.number
                                                                    ,d.id as d_id
                                                                    ,d.name
                                                                    ,d.phone
                                                                FROM employees e
                                                                JOIN departments d ON d.id = e.department_id
                                                                JOIN passports p ON p.id = e.passport_id";
    
    public const string GetAllEmployeesWithDependenciesByCompany = $@"SELECT   e.id  
                                                                    ,e.name
                                                                    ,e.surname
                                                                    ,e.phone
                                                                    ,e.company_id
                                                                    ,p.id as p_id
                                                                    ,p.type
                                                                    ,p.number
                                                                    ,d.id as d_id
                                                                    ,d.name
                                                                    ,d.phone
                                                                FROM employees e
                                                                JOIN departments d ON d.id = e.department_id
                                                                JOIN passports p ON p.id = e.passport_id
                                                                WHERE e.company_id = @company_id";
    
    public const string GetAllEmployeesWithDependenciesByDepartment = $@"SELECT   e.id  
                                                                    ,e.name
                                                                    ,e.surname
                                                                    ,e.phone
                                                                    ,e.company_id
                                                                    ,p.id as p_id
                                                                    ,p.type
                                                                    ,p.number
                                                                    ,d.id as d_id
                                                                    ,d.name
                                                                    ,d.phone
                                                                FROM employees e
                                                                JOIN departments d ON d.id = e.department_id
                                                                JOIN passports p ON p.id = e.passport_id
                                                                WHERE d.name = @department_name";
    #endregion
    
    #region Department
    public const string AddDepartment = @"INSERT INTO departments (name, phone)
                                          VALUES (@Name, @Phone)
                                              RETURNING id;";
    
    public const string UpdateDepartment = @"UPDATE departments 
                                           SET 
                                               name = COALESCE(@Name, name),
                                               phone = COALESCE(@Phone, phone)
                                           WHERE id = (SELECT department_id FROM employees WHERE id = @id)
                                           RETURNING id;";
    #endregion
    
    #region Passport
    public const string AddPassport = @"INSERT INTO passports (type, number)
                                          VALUES (@Type, @Number)
                                              RETURNING id;";
    
    public const string UpdatePassport = @"UPDATE passports 
                                           SET 
                                               type = COALESCE(@Type, type),
                                               number = COALESCE(@Number, number)
                                           WHERE id = (SELECT passport_id FROM employees WHERE id = @id)
                                           RETURNING id;";
    #endregion
}