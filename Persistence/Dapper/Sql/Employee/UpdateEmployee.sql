UPDATE employees
SET
    name = COALESCE(@name, name),
    surname = COALESCE(@surname, surname),
    phone = COALESCE(@phone, phone),
    company_id = COALESCE(@company_id, company_id),
    passport_id = COALESCE(@passport_id, passport_id),
    department_id = COALESCE(@department_id, department_id),
WHERE id = @id;
