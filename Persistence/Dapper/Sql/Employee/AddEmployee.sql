INSERT INTO employees (name, surname, phone, company_id, passport_id, department_id)
VALUES (@name, @surname, @phone, @company_id, @passport_id, @department_id)
    RETURNING id;